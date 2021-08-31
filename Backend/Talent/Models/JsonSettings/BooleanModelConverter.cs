using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Talent.Models.JsonSettings
{
    public class BooleanModelConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsSubclassOf(typeof(BooleanModel));
        }
        
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            JToken jToken = JToken.FromObject(value);
            if (jToken.Type != JTokenType.Object)
            {
                jToken.WriteTo(writer);
            }
            else
            {
                AddTypePropertyToWriter(writer, value, jToken);
            }
        }
        
        
        
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue,
            JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);
            var type = jObject["$type"]?.Value<string>();
            return GetBoolObject(type, jObject);
        }
        private BooleanModel? GetBoolObject(string? type, JObject jObject)
        {
            jObject.Remove("$type");
            if (IsBoolPhrase(type))
            {
                var phrase = DeserializePhrase(jObject);
                return phrase;
            }
            if (IsBoolExpression(type))
            {
                return DeserializeExpression(jObject);
            }
            return null;
        }

        private static BooleanExpressionModel? DeserializeExpression(JObject jObject)
        {
            return jObject.ToObject<BooleanExpressionModel>();
        }

        private BooleanPhraseModel DeserializePhrase(JObject jObject)
        {
            var phrase = new BooleanPhraseModel();
            phrase.OperatorType = (BooleanOperator) jObject["OperatorType"].Value<int>();
            phrase.Children = GetChildrenOfPhrase(jObject);
            return phrase;
        }
        
        private List<BooleanModel> GetChildrenOfPhrase(JObject jObject)
        {
            var childrenList = new List<BooleanModel>();
            var children = (JArray) jObject["Children"];
            foreach (var child in children)
            {
                var childType = child["$type"].Value<string>();
                var childObject = GetBoolObject(childType, (JObject) child);
                childrenList.Add(childObject);
            }
            return childrenList;
        }

        private bool IsBoolExpression(string? type)
        {
            return type == GetBoolModelType<BooleanExpressionModel>();
        }
        
        private  bool IsBoolPhrase(string? type)
        {
            return type == GetBoolModelType<BooleanPhraseModel>();
        }


        private void AddTypePropertyToWriter(JsonWriter writer, object? value, JToken jToken)
        {
            JObject jObject = (JObject) jToken;
            Type type = value?.GetType();
            jObject.AddFirst(new JProperty("$type", GetBoolModelType(type)));
            if (value is BooleanPhraseModel)
            {
                var phrase = (BooleanPhraseModel) value;
                string serilizedChildren = JsonConvert.SerializeObject(phrase.Children,settings: BooleanModel.GetSettings());
                jObject.Remove("Children");
                jObject.Add(new JProperty("Children",serilizedChildren.Replace("\\","")));
            }

            jObject.WriteTo(writer);
        }
        
        
        private  string GetBoolModelType<T>() where T:BooleanModel
        {
            Type typeT = typeof(T);
            return GetBoolModelType(typeT);
        }
        
        private  string GetBoolModelType(Type typeT)
        {
            if (typeT == typeof(BooleanExpressionModel))
            {
                return "expression";
            }
        
            if (typeT == typeof(BooleanPhraseModel))
            {
                return "phrase";
            }
        
            return "";
        }
     
    }
}
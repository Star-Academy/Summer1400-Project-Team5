using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Talent.Models.Boolean;

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
            if(!(value is BooleanModel)) return;
            JToken token = JToken.FromObject(value);
            if (token.Type != JTokenType.Object)
            {
                token.WriteTo(writer);
            }
            else
            {
                JObject jObject = (JObject) token;
                AddBoolModelToJObject((BooleanModel)value, jObject);
                jObject.WriteTo(writer);
            }
            
        }

        private void AddBoolModelToJObject(BooleanModel boolModel, JObject jObject)
        {
            var type = GetBoolModelType(boolModel.GetType());
            jObject.AddFirst(new JProperty("$type", type));
            if (IsBoolPhrase(type))
            {
                AddChildrenToJObject(boolModel, jObject);
            }
        }

        private void AddChildrenToJObject(BooleanModel boolModel, JObject jObject)
        {
            var children = ((BooleanPhraseModel) boolModel).Children;
            var jArray = new JArray();
            foreach (var child in children)
            {
                AddChildToJArray(child, jArray);
            }
            jObject.Remove("Children");
            jObject.Add(new JProperty("Children", jArray));
        }

        private void AddChildToJArray(BooleanModel child, JArray jArray)
        {
            JObject childObject = JObject.FromObject(child);
            AddBoolModelToJObject(child, childObject);
            jArray.Add(childObject);
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
                return DeserializePhrase(jObject);
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
            phrase.OperatorType = GetOperatorTypeOfPhrase(jObject);
            phrase.Children = GetChildrenOfPhrase(jObject);
            return phrase;
        }

        private static BooleanOperator GetOperatorTypeOfPhrase(JObject jObject)
        {
            return (BooleanOperator) jObject["OperatorType"].Value<int>();
        }

        private List<BooleanModel> GetChildrenOfPhrase(JObject jObject)
        {
            var childrenJsonArray = (JArray) jObject["Children"];
            var childrenList = GetChildrenObjects(childrenJsonArray);
            return childrenList;
        }

        private List<BooleanModel> GetChildrenObjects(JArray childrenJsonArray)
        {
            var childrenList = new List<BooleanModel>();
            foreach (var child in childrenJsonArray)
            {
                var childType = child["$type"]?.Value<string>();
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
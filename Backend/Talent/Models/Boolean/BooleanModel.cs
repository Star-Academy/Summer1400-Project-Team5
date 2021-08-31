using System.Collections.Generic;
using Newtonsoft.Json;
using Talent.Models.JsonSettings;

namespace Talent.Models
{
    public class BooleanModel : IChekable
    {
        

        public static JsonSerializerSettings GetSettings()
        {
            var setting = new JsonSerializerSettings()
            {
                Converters = new List<JsonConverter>()
                {
                    new BooleanModelConverter()
                }
            };
            return setting;
        }

        public bool CheckCondition<T>(T o)
        {
            throw new System.NotImplementedException();
        }
    }
    
    
}
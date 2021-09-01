using System.Collections.Generic;
using Newtonsoft.Json;

namespace Talent.Models.JsonSettings
{
    public static class BooleanModelJsonSetting
    {
        static BooleanModelJsonSetting()
        {
            Settings= new JsonSerializerSettings()
            {
                Converters = new List<JsonConverter>()
                {
                    new BooleanModelConverter()
                }
            };
        }
        public static JsonSerializerSettings Settings { get; set; } 
        

    }
}
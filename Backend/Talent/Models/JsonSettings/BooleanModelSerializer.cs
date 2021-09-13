using Newtonsoft.Json;
using Talent.Models.Boolean;

namespace Talent.Models.JsonSettings
{
    public class BooleanDeserializer
    {
        public static T Deserialize<T>(string json) where T: BooleanModel
        {
            return JsonConvert.DeserializeObject<T>(json, BooleanModelJsonSetting.Settings);
        }

        public static string Serialize<T>(T obj) where T: BooleanModel
        {
            return JsonConvert.SerializeObject(obj, BooleanModelJsonSetting.Settings);
        }
    }
}
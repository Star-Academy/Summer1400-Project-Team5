using Newtonsoft.Json;
using Talent.Models.Boolean;

namespace Talent.Models.JsonSettings
{
    public class BooleanDeserializer
    {
        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, BooleanModelJsonSetting.Settings);
        }
    }
}
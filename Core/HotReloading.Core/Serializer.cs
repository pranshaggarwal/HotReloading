using System;
using Newtonsoft.Json;

namespace HotReloading.Core
{
    public static class Serializer
    {
        private static JsonSerializerSettings JsonSettings
        {
            get
            {
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                };
                return settings;
            }
        }

        public static TType DeserializeJson<TType>(string json)
        {
            return JsonConvert.DeserializeObject<TType>(json, JsonSettings);
        }

        public static string SerializeJson<TType>(TType obj)
        {
            return JsonConvert.SerializeObject(obj, JsonSettings);
        }
    }
}
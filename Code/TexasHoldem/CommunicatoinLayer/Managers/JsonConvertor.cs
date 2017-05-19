using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommunicatoinLayer.Managers
{
    public static class JsonConvertor
    {
        public static string SerializeObject(object serializeObject)
        {
            var indented = Formatting.Indented;
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            };
            string serialized = JsonConvert.SerializeObject(serializeObject, settings);
            return serialized;
        }

        public static void DeserializeObject<T>(string serialized , out T obj)
        {
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            };
            obj = JsonConvert.DeserializeObject<T>(serialized, settings);
        }
    }
}
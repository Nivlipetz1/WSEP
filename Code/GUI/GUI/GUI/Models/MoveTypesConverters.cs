using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Models
{
    public static class MoveTypesConverter
    {
        public static T deserializeObject<T>(string serialize)
        {
            JsonConverter[] converters = { new MoveConverter() };
            var obj = JsonConvert.DeserializeObject<T>(serialize, new JsonSerializerSettings() { Converters = converters });
            return obj;
        }
    }

    public class MoveConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Move));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            if (jo["type"].Value<string>() == "NewCardMove")
                return jo.ToObject<NewCardMove>(serializer);

            if (jo["type"].Value<string>() == "GameStartMove")
                return jo.ToObject<GameStartMove>(serializer);

            if (jo["type"].Value<string>() == "BetMove")
                return jo.ToObject<BetMove>(serializer);

            if (jo["type"].Value<string>() == "FoldMove")
                return jo.ToObject<FoldMove>(serializer);

            if (jo["type"].Value<string>() == "EndGameMove")
                return jo.ToObject<EndGameMove>(serializer);

            return null;
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}

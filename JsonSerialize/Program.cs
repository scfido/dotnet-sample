using System;
using System.Collections.Specialized;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonSerialize
{
    /// <summary>
    /// 自定义JsonConverter示例
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Test();
            Console.WriteLine("Hello World!");
        }

        static void Test()
        {
            var user = new User();
            user.Name = "abc";
            user.Props = new NameValueCollection() {
                { "A", "1" },
                { "A", "1.1" },
                { "B", "2" }
            };
            user.Props["C"] = "3";
            user.Props= null;

            var json = JsonConvert.SerializeObject(user, Formatting.Indented);
            var newUser = JsonConvert.DeserializeObject<User>(json);
        }
    }

    class User
    {
        public string Name { get; set; }

        [JsonConverter(typeof(NameValueCollectionConverter))]
        public NameValueCollection Props { get; set; }
    }

    public class NameValueCollectionConverter : JsonConverter<NameValueCollection>
    {
        public override NameValueCollection ReadJson(JsonReader reader, Type objectType, NameValueCollection existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value == null || reader.TokenType != JsonToken.StartObject)
                return null;

            var nvc = new NameValueCollection();
            while (true)
            {
                reader.Read();
                if (reader.TokenType == JsonToken.EndObject)
                    break;

                var key = reader.Value.ToString();
                reader.Read();
                var value = reader.Value.ToString();
                nvc.Add(key, value);
            };

            return nvc;
        }

        public override void WriteJson(JsonWriter writer, NameValueCollection value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            writer.WriteStartObject();
            foreach (string key in value)
            {
                writer.WritePropertyName(key);
                writer.WriteValue(value[key]);
            }
            writer.WriteEndObject();
        }
    }
}

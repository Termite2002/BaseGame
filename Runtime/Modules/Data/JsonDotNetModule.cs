using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Termite.BaseGame
{
    public class JsonDotNetModule : IJsonService
    {
        public string ToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public T FromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public JObject ToJObject(string json)
        {
            return JObject.Parse(json);
        }
    }
}

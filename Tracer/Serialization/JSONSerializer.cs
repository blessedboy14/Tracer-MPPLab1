using Tracing;
using Newtonsoft.Json;

namespace Serialization
{
    public class JSONSerializer<T> : ISerializer<T>
    {
        public string Serialize(T input)
        {
            string jsonData = JsonConvert.SerializeObject(input, Formatting.Indented);
            return jsonData;
        }
    }
}

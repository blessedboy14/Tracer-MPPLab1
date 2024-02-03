using Tracing;
using Newtonsoft.Json;

namespace Serialization
{
    public class JSONSerializer : ISerializer
    {
        public string Serialize(TraceResult input)
        {
            string jsonData = JsonConvert.SerializeObject(input, Formatting.Indented);
            return jsonData;
        }
    }
}

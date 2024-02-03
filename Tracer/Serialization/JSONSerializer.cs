using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

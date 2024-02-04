using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Tracing {
    public class TraceResult
    {
        [JsonIgnore]
        [XmlIgnore]
        public ConcurrentDictionary<int, ThreadTrace> threads;
        [XmlElement(ElementName = "threads")]
        [JsonProperty(propertyName:"threads")]
        public List<ThreadTrace> traces = new List<ThreadTrace>();

        public TraceResult()
        {
            threads = new ConcurrentDictionary<int, ThreadTrace>();
        }
    }
}

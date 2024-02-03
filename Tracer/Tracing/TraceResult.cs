using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Tracing {
    public class TraceResult
    {
        [JsonIgnore]
        public ConcurrentDictionary<int, ThreadTrace> threads;
        public List<ThreadTrace> traces = new List<ThreadTrace>();

        public TraceResult()
        {
            threads = new ConcurrentDictionary<int, ThreadTrace>();
        }
    }
}

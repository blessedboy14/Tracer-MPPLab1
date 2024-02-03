using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Tracing {
    public class TraceResult
    {
        public ConcurrentDictionary<int, ThreadTrace> threads = new ConcurrentDictionary<int, ThreadTrace>();

        public TraceResult()
        {
            threads = new ConcurrentDictionary<int, ThreadTrace>();
        }
    }
}

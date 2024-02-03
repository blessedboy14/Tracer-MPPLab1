using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Reflection;
using System.Threading;
using System.Collections.Concurrent;


namespace Tracing
{
    public class CustomTracer : ITracer
    {
        public Stopwatch watch;
        private ConcurrentDictionary<int, List<Stopwatch>> timers = new ConcurrentDictionary<int, List<Stopwatch>>();
        private TraceResult info = new TraceResult();
        private ConcurrentDictionary<int, int> methods = new ConcurrentDictionary<int, int>();

        private readonly object balanceLock = new object();

        private TraceResult traceResult;

        public CustomTracer()
        {
            watch = new Stopwatch();
        }

        public TraceResult GetTraceResult()
        {
        }

        public void StartTrace()
        {
            StackTrace trace = new StackTrace();
            StackFrame calling = trace.GetFrame(1);
            var method = calling.GetMethod();
            string methodName = method.Name;
            string className = method.DeclaringType.FullName;
            int threadID = Thread.CurrentThread.ManagedThreadId;
            ThreadTrace curTrace = new ThreadTrace();
            if (info.threads.TryAdd(threadID, curTrace))
            {
                info.threads[threadID].threadId = threadID;
                methods.TryAdd(threadID, 0);
                timers.TryAdd(threadID, new List<Stopwatch>());
            }
        }

        public void StopTrace()
        {
        }
    }
}

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
        private ConcurrentDictionary<int, int> methodsInThread = new ConcurrentDictionary<int, int>();

        private void CreateMethodStack(int threadID, string methodName, string className)
        {
            List<MethodTrace> methodList = new List<MethodTrace>();
            methodList = info.threads[threadID].methodsTrace;
            for (int i = 1; i < methodsInThread[threadID];i++)
            {
                methodList = methodList[methodList.Count - 1].stack;
            }
            MethodTrace methodInfo = new MethodTrace();
            methodInfo.methodName = methodName;
            methodInfo.className = className;
            methodList.Add(methodInfo);
        }

        public TraceResult GetTraceResult()
        {
            foreach(KeyValuePair<int, ThreadTrace> thread in info.threads)
            {
                long time = 0;
                foreach(MethodTrace method in thread.Value.methodsTrace) 
                {
                    time += method.executeInMillis;
                }
                thread.Value.executeTime = time;
            }
            return info;
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
                methodsInThread.TryAdd(threadID, 0);
                timers.TryAdd(threadID, new List<Stopwatch>());
            }
            methodsInThread[threadID]++;
            timers[threadID].Add(new Stopwatch());
            CreateMethodStack(threadID, methodName, className);
            timers[threadID][timers[threadID].Count - 1].Start();
        }

        public void StopTrace()
        {
            int threadID = Thread.CurrentThread.ManagedThreadId;
            timers[threadID][timers[threadID].Count - 1].Stop();
            List<MethodTrace> methodsList = new List<MethodTrace>();
            methodsList = info.threads[threadID].methodsTrace;
            for (int i = 1; i < methodsInThread[threadID]; i++)
            {
                methodsList = methodsList[methodsList.Count - 1].stack;
            }
            methodsList[methodsList.Count - 1].executeInMillis = timers[threadID][timers[threadID].Count - 1].ElapsedMilliseconds;
            timers[threadID].Remove(timers[threadID][timers[threadID].Count - 1]);
            methodsInThread[threadID]--;
        }
    }
}

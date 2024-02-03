using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Tracing;
using Serialization;

class TracerLab
{
    public static CustomTracer tracer = new CustomTracer();
    static public void Main(string[] args)
    {
        ISerializer jsonSerializer = new JSONSerializer();
        Thread thread1 = new Thread(exampleMethod);
        Thread thread2 = new Thread(exampleMethod);
        thread1.Start();
        thread2.Start();
        exampleMethod();
        thread1.Join();
        thread2.Join();
        TraceResult res = tracer.GetTraceResult();
        Console.WriteLine(jsonSerializer.Serialize(res));
        int i = 0;
    }

    static public void exampleMethod()
    {
        tracer.StartTrace();
        Thread.Sleep(2000);
        tracer.StopTrace();
    }
}

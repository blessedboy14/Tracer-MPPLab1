using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Tracing;

class TracerLab
{
    public static CustomTracer tracer = new CustomTracer();
    static public void Main(string[] args)
    {
        exampleMethod();
        TraceResult res = tracer.GetTraceResult();
        int i = 0;
    }

    static public void exampleMethod()
    {
        tracer.StartTrace();
        Thread.Sleep(2000);
        tracer.StopTrace();
    }
}

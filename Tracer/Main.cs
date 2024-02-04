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
    private static IPrinter console = new ConsolePrinter();
    private static IPrinter file = new FilePrinter(@"C:\Users\User\Desktop\log.txt");
    private static ISerializer<TraceResult> jsonSerializer = new JSONSerializer<TraceResult>();
    private static ISerializer<TraceResult> xmlSerializer = new XMLSerializer<TraceResult>();
    static public void Main(string[] args)
    {
        Thread thread1 = new Thread(exampleMethod);
        Thread thread2 = new Thread(exampleMethod);
        thread1.Start();
        thread2.Start();
        exampleMethod();
        thread1.Join();
        thread2.Join();
        TraceResult res = tracer.GetTraceResult();
        console.Print(jsonSerializer.Serialize(res));
        file.Print(jsonSerializer.Serialize(res));
        Console.WriteLine(xmlSerializer.Serialize(res));
    }

    static public void exampleMethod()
    {
        tracer.StartTrace();
        Thread.Sleep(2000);
        tracer.StopTrace();
    }
}

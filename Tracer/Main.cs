using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

class TracerLab
{
    public static ITracer tracer = new CustomTracer();
    public static Foo _foo;
    static public void Main(string[] args)
    {
        _foo = new Foo(tracer);
        _foo.MyMethod();
        tracer.GetTraceResult();
        int i = 0;
    }

    static public void exampleMethod()
    {
        tracer.StartTrace();
        for (long i = 0; i < 100000000; i++)
        {
            var t = (i / 2 * i / 3) % 50 * 100;
        }
        tracer.StopTrace();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Bar
{
    private ITracer _tracer;
    public Bar(ITracer tracer) 
    { 
        _tracer = tracer;
    }   
    internal void InnerMethod()
    {
        _tracer.StartTrace();
        for (long i = 0; i < 50000000; i++)
        {
            var t = (i / 2 * i / 3) % 50 * 100;
        }
        _tracer.StopTrace();
    }
}

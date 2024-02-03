using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Foo
{
    private ITracer _tracer;
    private Bar _bar;
    internal Foo(ITracer tracer)
    {
        _tracer = tracer;
        _bar = new Bar(tracer);
    }   
    public void MyMethod()
    {
        _tracer.StopTrace();
        _bar.InnerMethod();
        _tracer.StopTrace();
    }
}

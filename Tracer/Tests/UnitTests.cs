using NUnit.Framework;
using Tracing;
using System.Threading;
using System.Collections.Generic;
using System;
using NUnit.Framework.Legacy;

namespace Testing
{
    [TestFixture]
    public class UnitTests
    {
        static CustomTracer tracer;


        [SetUp]
        public void SetUp()
        {
            tracer = new CustomTracer();
        }

        static void exampleMethod()
        {
            tracer.StartTrace();
            Thread.Sleep(1000);
            tracer.StopTrace();
        }
        
        public static class TestClass
        {
            public static void testMethod()
            {
                tracer.StartTrace();
                exampleMethod();
                Thread.Sleep(1000);
                tracer.StopTrace();
            }
            public static void recursionMethod(int i)
            {
                tracer.StartTrace();
                i--;
                exampleMethod();
                if (i > 0)
                {
                    recursionMethod(i);
                }
                tracer.StopTrace();
            }
        }

        static void RecursionMethod(int i)
        {
            tracer.StartTrace();
            i--;
            Thread.Sleep(10);
            if (i > 0)
            {
                RecursionMethod(i);
            }
            tracer.StopTrace();
        }

        [Test]
        public void OneThread__FavourableTestEasyCase()
        {
            exampleMethod();
            var res = tracer.GetTraceResult();
            ThreadTrace threadTrace = res.traces[0];
            Console.WriteLine(threadTrace.ToString());
            ClassicAssert.AreEqual(threadTrace.methodsTrace[0].methodName, "exampleMethod");
            ClassicAssert.AreEqual(threadTrace.methodsTrace[0].className, typeof(UnitTests).FullName);
        }

        [Test]
        public void OneThread__RecursionTestCase()
        {
            RecursionMethod(3);
            var res = tracer.GetTraceResult();
            ThreadTrace threadTrace = res.traces[0];
            ClassicAssert.AreEqual(res.traces.Count, 1);
            List<MethodTrace> stackStart = threadTrace.methodsTrace;
            ClassicAssert.IsTrue(stackStart.Count > 0);
            ClassicAssert.AreEqual(stackStart[0].methodName, "RecursionMethod");
            stackStart = stackStart[0].stack;
            ClassicAssert.IsTrue(stackStart.Count > 0);
            stackStart = stackStart[0].stack;
            ClassicAssert.IsTrue(stackStart.Count > 0);
            ClassicAssert.AreEqual(stackStart[0].methodName, "RecursionMethod");
        }

        [Test]
        public void TwoThread__EasyTestCase()
        {
            exampleMethod();
            Thread newThread = new Thread(exampleMethod);
            newThread.Start();
            newThread.Join();
            var res = tracer.GetTraceResult();
            ClassicAssert.AreEqual(res.traces.Count, 2);
            ClassicAssert.AreEqual(res.threads[Thread.CurrentThread.ManagedThreadId].methodsTrace[0].methodName, "exampleMethod");
            ClassicAssert.AreEqual(res.threads[newThread.ManagedThreadId].methodsTrace[0].methodName, "exampleMethod");
        }


        [Test]
        public void Inserting__EasyTestCase()
        {
            TestClass.testMethod();
            var res = tracer.GetTraceResult();
            var tTrace = res.traces[0];
            ClassicAssert.AreEqual(tTrace.methodsTrace[0].methodName, "testMethod");
            ClassicAssert.AreEqual(tTrace.methodsTrace[0].className, typeof(TestClass).FullName);
            var mTrace = tTrace.methodsTrace[0];
            ClassicAssert.AreEqual(mTrace.stack[0].methodName, "exampleMethod");
            ClassicAssert.AreEqual(mTrace.stack[0].className, typeof(UnitTests).FullName);
        }

        [Test]
        public void InsertingWithRecursion__TestCase()
        {
            TestClass.recursionMethod(3);
            var res = tracer.GetTraceResult();
            var tTrace = res.traces[0];
            ClassicAssert.AreEqual(tTrace.methodsTrace[0].methodName, "recursionMethod");
            ClassicAssert.AreEqual(tTrace.methodsTrace[0].className, typeof(TestClass).FullName);
            var mTrace = tTrace.methodsTrace[0];
            ClassicAssert.AreEqual(mTrace.stack[0].methodName, "exampleMethod");
            ClassicAssert.AreEqual(mTrace.stack[0].className, typeof(UnitTests).FullName);
            mTrace = mTrace.stack[1];
            ClassicAssert.AreEqual(mTrace.methodName, "recursionMethod");
            ClassicAssert.AreEqual(mTrace.className, typeof(TestClass).FullName);
            mTrace = mTrace.stack[0];
            ClassicAssert.AreEqual(mTrace.methodName, "exampleMethod");
            ClassicAssert.AreEqual(mTrace.className, typeof(UnitTests).FullName);
            ClassicAssert.IsTrue(mTrace.stack.Count == 0);
        }
    }
}

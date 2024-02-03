using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracing
{
    public class MethodTrace
    {
        public string methodName;
        public string className;
        public long executeInMillis;
        public List<MethodTrace> stack = new List<MethodTrace>();
    }

}
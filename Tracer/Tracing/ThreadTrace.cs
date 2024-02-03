using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracing
{
    public class ThreadTrace
    {
        public List<MethodTrace> methodsTrace = new List<MethodTrace>();
        public int threadId;
        public long executeTime;
    }
}

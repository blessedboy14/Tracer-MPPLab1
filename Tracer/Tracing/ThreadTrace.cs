using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracing
{
    public class ThreadTrace
    {
        public int threadId;
        public long executeTime;
        public List<MethodTrace> methodsTrace = new List<MethodTrace>();
    }
}

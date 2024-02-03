using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialization 
{
    public interface IPrinter
    {
        void ConsolePrint(string message);
        void FilePrint(string message, string fileName);
    }
}

using System;

namespace Serialization
{
    public class ConsolePrinter : IPrinter
    {
        public void Print(string message) 
        { 
            Console.WriteLine(message);
        }
    }
}

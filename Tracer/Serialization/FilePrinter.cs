using System;
using System.IO;

namespace Serialization
{
    public class FilePrinter : IPrinter
    {

        private string fileName;

        public FilePrinter(string fileName)
        {
            this.fileName = fileName;
        }   

        public void Print(string message)
        {
            try
            {
                File.WriteAllText(fileName, string.Empty);
                File.WriteAllText(fileName, message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to write to file cause of: " + ex.ToString());
            }
        }
    }
}

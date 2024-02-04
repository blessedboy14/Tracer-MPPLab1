using Tracing;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace Serialization
{
    public class XMLSerializer<T> : ISerializer<T>
    {
        public string Serialize(T input)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (StringWriter sw = new StringWriter())
            {
                using (XmlTextWriter writer = new XmlTextWriter(sw) { Formatting = Formatting.Indented })
                {
                    xmlSerializer.Serialize(writer, input);
                    return sw.ToString();
                }
            }
        }
    }
}

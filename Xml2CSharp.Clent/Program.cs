using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using CommandLine;
using CommandLine.Text;
using Xml2CSharp.ClassWriters;

namespace Xml2CSharp.Clent
{
    class Program
    {
        static void Main(string[] args)
        {
            var xml = File.ReadAllText(@"C:\Users\Lenovo 1\Desktop\XmlToCSharp\Xml2CSharp.Clent\TestXML.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(Realestates));
            using (StringReader reader = new StringReader(xml))
            {
                var test = (Realestates)serializer.Deserialize(reader);
            }

        }
	}




}

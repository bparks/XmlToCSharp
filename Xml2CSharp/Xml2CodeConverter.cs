using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Xml2CSharp
{
    public class Xml2CodeConverter
    {
        IClassWriter _writer = null;

        public Xml2CodeConverter(IClassWriter writer)
        {
            _writer = writer;
        }

        public string Convert(string xml, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                var xElement = XElement.Parse(xml);
                var elements = xElement.ExtractClassInfo();
                return _writer.Write(elements, xElement.Name.LocalName.ToString());
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message + Environment.NewLine + ex.StackTrace;
                throw;
            }
           
        }

    }
}
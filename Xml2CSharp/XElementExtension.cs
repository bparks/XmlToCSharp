using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Xml2CSharp
{
    public static class XElementExtension
    {
        public static IEnumerable<Class> ExtractClassInfo(this XElement element)
        {
            var @classes = new HashSet<Class>();
            ElementToClass(element, classes);
            return @classes;
        }

        public static bool IsClass(this XElement element)
        {
            return element.HasAttributes || element.HasElements;
        }

        private static Class ElementToClass(XElement xElement, ICollection<Class> classes)
        {

            var extractedFields = ExtractFields(xElement, classes);
            var replacedDuplicatesWithLists =  ReplaceDuplicatesWithLists(extractedFields).ToList();
            var @class = new Class
            {
                Name = xElement.Name.LocalName,
                XmlName = xElement.Name.LocalName,
                Fields = replacedDuplicatesWithLists,
                Namespace = xElement.Name.NamespaceName
            };

            SafeName(@class, @classes);

            if (xElement.Parent == null || (!@classes.Contains(@class) && @class.Fields.Any()))
                @classes.Add(@class);

            return @class;

        }

        private static IEnumerable<Field> ExtractFields(XElement xElement, ICollection<Class> classes)
        {
            foreach (var element in xElement.Elements().ToList())
            {
                var tempClass = ElementToClass(element, classes);
                var xmlType = element.IsClass() ? XmlType.Class : GetXmlType(element.Value);
                var classXmlType = GetXmlType(element.Value);
                yield return new Field
                {
                    Name = tempClass.Name,
                    InternalXmlType = xmlType,
                    XmlType = classXmlType,
                    XmlName = tempClass.XmlName,
                    XmlAttributeType = XmlAttributeType.Element,
                    Namespace = tempClass.Namespace
                };
            }

            foreach (var attribute in xElement.Attributes().ToList())
            {
                yield return new Field
                {
                    Name = attribute.Name.LocalName,
                    XmlName = attribute.Name.LocalName,
                    InternalXmlType = GetXmlType(attribute.Value),
                    XmlType = GetXmlType(attribute.Value),
                    XmlAttributeType = XmlAttributeType.Attribute,
                    Namespace = attribute.Name.NamespaceName
                };
            }

            if (xElement.Attributes().Any() && !string.IsNullOrEmpty(xElement.Value.Trim()))
            {
                yield return new Field
                {
                    Name = "text",
                    XmlName = "text",
                    InternalXmlType = GetXmlType(xElement.Value),
                    XmlType = GetXmlType(xElement.Value),
                    XmlAttributeType = XmlAttributeType.Attribute,
                    Namespace = xElement.Name.NamespaceName,
                    IsValue = true
                };
            }
        }

        private static IEnumerable<Field> ReplaceDuplicatesWithLists(IEnumerable<Field> fields)
        {
            // It's an array of int -> and then handle it in code
            return fields.GroupBy(field => field.Name, field => field,
                (key, g) =>
                    g.Count() > 1
                        ? new Field()
                        {
                            Name = key,
                            Namespace = g.First().Namespace,
                            XmlName = g.First().Name,
                            XmlType = XmlType.Array,
                            InternalXmlType = g.First().InternalXmlType,
                            XmlAttributeType = XmlAttributeType.Element
                        } :
                        g.First()).ToList();
        }

        private static void SafeName(Class @class, IEnumerable<Class> classes)
        {
            var count = classes.Count(c => c.XmlName == @class.Name);
            if (count > 0 && !@classes.Contains(@class))
            {
                @class.Name = StripBadCharacters(@class) + (count + 1);
            }
            else
            {
                @class.Name = StripBadCharacters(@class);
            }
        }

        private static string StripBadCharacters(Class @class)
        {
            return @class.Name.Replace("-", "");
        }

        private static XmlType GetXmlType(string value)
        {
            bool boolobj; float floatobj; DateTime dateobj; int intobj;

            // TEST BOOL
            if (bool.TryParse(value, out boolobj))
                return XmlType.Boolean;

            // TEST DATETIME
            if (DateTime.TryParse(value, out dateobj))
                return XmlType.DateTime;

            // TEST INT
            if (int.TryParse(value, out intobj))
                return XmlType.Integer;

            // TEST FLOAT
            if (float.TryParse(value, out floatobj))
            return XmlType.Float;
         
            // TEST string
            if (!string.IsNullOrEmpty(value))
                return XmlType.String;

            return XmlType.Anything;
        }
    }
}
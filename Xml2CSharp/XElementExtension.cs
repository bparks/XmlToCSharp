using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Xml2CSharp
{
    public static class XElementExtension
    {
        public static List<Class> ExtractClassInfo(this XElement element)
        {
            var @classes = new List<Class>();
            ElementToClass(element, classes);
            classes = HandleDuplicates(classes);
            return @classes;
        }

        public static List<Class> HandleDuplicates(List<Class> classes)
        {
            List<Class> classesRet = new List<Class>();
            var groups = classes.GroupBy(p => p.XmlName);

            foreach (var group in groups)
            {
                var groupKey = group.Key;
                if (group.Count() > 1)
                {
                    List<Field> groupedFields = new List<Field>();
                    Class classtoadd = group.FirstOrDefault();

                    foreach (var groupedItem in group)
                    {
                        foreach (var field in groupedItem.Fields)
                        {

                            if (!groupedFields.Exists(p => p.Name == field.Name))
                                groupedFields.Add(field);
                            else
                            {
                                // if different types -> then rename and then add
                                var sameNameDiffernetTypes = groupedFields.Where(p => p.Name == field.Name && (p.XmlType != field.XmlType || p.InternalXmlType != field.InternalXmlType));
                                if (sameNameDiffernetTypes.Any())
                                {
                                    // Check if its an array or class -> discard the field and put the array instead
                                    if (field.XmlType == XmlType.Class && sameNameDiffernetTypes.FirstOrDefault().XmlType != XmlType.Array)
                                    {
                                        groupedFields.RemoveAll( p => true);
                                        groupedFields.Add(field);

                                    }
                                    if (field.XmlType == XmlType.Array)
                                    {
                                        groupedFields.RemoveAll(p => true);
                                        groupedFields.Add(field);
                                    }
                                }
                            }
                        }
                    }

                    classtoadd.Fields = groupedFields;
                    classesRet.Add(classtoadd);
                }
                else
                {
                    classesRet.Add(group.FirstOrDefault());
                }
            }

            return classesRet;
        }

        public static bool IsClass(this XElement element)
        {
            return element.HasAttributes || element.HasElements;
        }

        private static Class ElementToClass(XElement xElement, List<Class> classes)
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

        private static List<Field> ExtractFields(XElement xElement, List<Class> classes)
        {
            List<Field> list = new List<Field>();
            var test = xElement.Name;
            var testelements = xElement.Elements();
            foreach (var element in xElement.Elements().ToList())
            {
                var tempClass = ElementToClass(element, classes);
                var xmlType = element.IsClass() ? XmlType.Class : GetXmlType(element.Value);
                list.Add(new Field
                {
                    Name = tempClass.Name,
                    InternalXmlType = xmlType,
                    XmlType = xmlType ,
                    XmlName = tempClass.XmlName,
                    XmlAttributeType = XmlAttributeType.Element,
                    Namespace = tempClass.Namespace
                });
            }

            foreach (var attribute in xElement.Attributes().ToList())
            {
                list.Add(new Field
                {
                    Name = attribute.Name.LocalName,
                    XmlName = attribute.Name.LocalName,
                    InternalXmlType = GetXmlType(attribute.Value),
                    XmlType = GetXmlType(attribute.Value),
                    XmlAttributeType = XmlAttributeType.Attribute,
                    Namespace = attribute.Name.NamespaceName
                });
            }

            if (xElement.Attributes().Any() && !string.IsNullOrEmpty(xElement.Value.Trim()))
            {
                list.Add(new Field
                {
                    Name = "text",
                    XmlName = "text",
                    InternalXmlType = GetXmlType(xElement.Value),
                    XmlType = GetXmlType(xElement.Value),
                    XmlAttributeType = XmlAttributeType.Attribute,
                    Namespace = xElement.Name.NamespaceName,
                    IsValue = true
                });
            }
            return list;
        }

        private static List<Field> ReplaceDuplicatesWithLists(List<Field> fields)
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

        private static void SafeName(Class @class, ICollection<Class> classes)
        {
            //var count = classes.Count(c => c.XmlName == @class.Name);
            //if (count > 0)
            //{
            //    // classes.FirstOrDefault( x => x.XmlName == @class.Name).Fields
            //    // Merge fields recursively in existing classes
            //    //@class.Name = StripBadCharacters(@class);
            //}
            //else
            //{
              @class.Name = StripBadCharacters(@class);
            //}
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
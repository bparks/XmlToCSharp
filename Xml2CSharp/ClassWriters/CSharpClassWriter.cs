using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Xml2CSharp.ClassWriters
{
    public class CSharpClassWriter : IClassWriter
    {
        Dictionary<string, object> _settings;
        public CSharpClassWriter(Dictionary<string, object> settings)
        {
            _settings = settings;
        }

        public List<string> ReservedKeywords => new List<string>() { "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class", "const", "continue", "decimal", "default", "delegate", "do", "double", "else", "enum", "event", "explicit", "extern", "false", "finally", "fixed", "float", "for", "foreach", "goto", "if", "implicit", "in", "int", "interface", "internal", "is", "lock", "long", "namespace", "new", "null", "object", "operator", "out", "override", "params", "private", "protected", "public", "readonly", "ref", "return", "sbyte", "sealed", "short", "sizeof", "stackalloc", "static", "string", "struct", "switch", "this", "throw", "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", "using", "virtual", "void", "volatile", "while" };

        public string Comment => @"// using System.Xml.Serialization;
// XmlSerializer serializer = new XmlSerializer(typeof({0}));
// using (StringReader reader = new StringReader(xml))
// {{
//    var test = ({0})serializer.Deserialize(reader);
// }}
";

        private bool RemoveXMLAttributes { get { return _settings != null && _settings.ContainsKey("RemoveXMLAttributes") && _settings["RemoveXMLAttributes"].ToString().ToLower() == "true"; } }
        private bool AddNamespaceAttributes { get { return _settings != null && _settings.ContainsKey("AddNamespaceAttributes") && _settings["AddNamespaceAttributes"].ToString().ToLower() == "true"; } }
        private bool UsePascalCase { get { return _settings != null && _settings.ContainsKey("UsePascalCase") && _settings["UsePascalCase"].ToString().ToLower() == "true"; } }
        private bool UseFields { get { return _settings != null && _settings.ContainsKey("UseFields") && _settings["UseFields"].ToString().ToLower() == "true"; } }

        public string GetTypeName(Field field, bool isInternal = false)
        {
            switch (isInternal ? field.InternalXmlType : field.XmlType)
            {
                case XmlType.Anything: return "object";
                case XmlType.Array: return "List<" + GetTypeName(field, true) + ">";
               // case XmlType.Dictionary: return "Dictionary<string, " + GetTypeName(type.InternalType, config) + ">";
                case XmlType.Boolean: return "bool";
                case XmlType.Float: return "double";
                case XmlType.Integer: return "int";
                case XmlType.Double: return "long";
                case XmlType.DateTime: return "DateTime";
                case XmlType.NonConstrained: return "object";
                case XmlType.NullableBoolean: return "bool?";
                case XmlType.NullableFloat: return "double?";
                case XmlType.NullableInteger: return "int?";
                case XmlType.NullableLong: return "long?";
                case XmlType.NullableDate: return "DateTime?";
                case XmlType.NullableSomething: return "object";
               // case XmlType.Object: return type.NewAssignedName; // You need to use this for setting the object in the whatever
                case XmlType.String: return "string";
                default: throw new System.NotSupportedException("Unsupported xml type");
            }
        }



        public string Write(IEnumerable<Class> classInfo, string rootName)
        {
            StringBuilder sb = new StringBuilder();

            // Add Top Comment
            if (!string.IsNullOrEmpty(Comment))
            {
                if (!UsePascalCase)
                {
                    if (ReservedKeywords.Contains(rootName)) // Check for reserved words in case no pascal
                        sb.AppendLine(string.Format(Comment, "@" + rootName));
                    else
                        sb.AppendLine(string.Format(Comment, rootName));
                }
                else
                    sb.AppendLine(string.Format(Comment, rootName.ToTitleCase()));
            }

            foreach (var @class in classInfo)
            {
                if (!RemoveXMLAttributes)
                {
                    if (AddNamespaceAttributes)
                        sb.AppendFormat("[XmlRoot(ElementName=\"{0}\", Namespace=\"{1}\")]{2}", @class.XmlName, @class.Namespace, Environment.NewLine);
                    else
                        sb.AppendFormat("[XmlRoot(ElementName=\"{0}\")]{1}", @class.XmlName, Environment.NewLine);
                }

                if (UsePascalCase)
                    @class.Name = @class.Name.ToTitleCase();

                if (!UsePascalCase) // Check for reserved words in case no pascal
                    if (ReservedKeywords.Contains(@class.Name)) @class.Name = "@" + @class.Name;

                sb.AppendFormat("public class {0} {{ {1}", @class.Name, Environment.NewLine);
                foreach (var field in @class.Fields)
                {
                    if (!RemoveXMLAttributes)
                    {
                        if (AddNamespaceAttributes)
                        {
                            if (field.IsValue)
                                sb.AppendFormat("{0}\t[XmlText] {0}", Environment.NewLine);
                            else
                            sb.AppendFormat("{3}\t[Xml{0}({0}Name=\"{1}\", Namespace=\"{2}\")] {3}", field.XmlAttributeType, field.XmlName, field.Namespace, Environment.NewLine);

                        }
                        else
                        {
                            if (field.IsValue)
                                sb.AppendFormat("{0}\t[XmlText] {0}", Environment.NewLine);
                            else
                                sb.AppendFormat("{2}\t[Xml{0}({0}Name=\"{1}\")] {2}", field.XmlAttributeType, field.XmlName, Environment.NewLine);
                        }
                    }

                    if (UsePascalCase)
                        field.Name = field.Name.ToTitleCase();

                    if (!UsePascalCase) // Check for reserved words in case no pascal
                        if (ReservedKeywords.Contains(field.Name)) field.Name = "@" + field.Name;

                    // If it's a class property then set the current name of the property as the name of the class
                    var xmltype = field.InternalXmlType == XmlType.Class ? field.Name: GetTypeName(field);
                    if (UseFields)
                        sb.AppendFormat("\tpublic {0} {1}; {2}", xmltype, field.Name, Environment.NewLine);
                    else
                        sb.AppendFormat("\tpublic {0} {1} {{ get; set; }} {2}", xmltype, field.Name, Environment.NewLine);
                }
                sb.AppendLine("}");
                sb.AppendLine("");
            }

            return sb.ToString();
        }
    }
}
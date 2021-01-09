using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xml2CSharp.ClassWriters
{
    public class JavaClassWriter : IClassWriter
    {
        Dictionary<string, object> _settings;
        public JavaClassWriter(Dictionary<string, object> settings)
        {
            _settings = settings;
        }

        public string GetTypeName(Field field, bool isInternal = false)
        {
            switch (isInternal ? field.InternalXmlType : field.XmlType)
            {
                case XmlType.Anything: return "Object";
                case XmlType.Array: return "List<" + GetTypeName(field, true) + ">";
                // case XmlType.Dictionary: return "Dictionary<string, " + GetTypeName(type.InternalType, config) + ">";
                case XmlType.Boolean: return "boolean";
                case XmlType.Float: return "double";
                case XmlType.Integer: return "int";
                case XmlType.Double: return "double";
                case XmlType.DateTime: return "Date";
                case XmlType.NonConstrained: return "object";
                case XmlType.NullableBoolean: return "boolean";
                case XmlType.NullableFloat: return "double";
                case XmlType.NullableInteger: return "int";
                case XmlType.NullableLong: return "double";
                case XmlType.NullableDate: return "Date";
                case XmlType.Class: return field.Name;
                case XmlType.NullableSomething: return "Object";
                // case XmlType.Object: return type.NewAssignedName; // You need to use this for setting the object in the whatever
                case XmlType.String: return "String";
                default: throw new System.NotSupportedException("Unsupported xml type");
            }
        }

        public string Comment => @"";
        private bool UseProperties { get { return _settings != null && _settings.ContainsKey("UseProperties") && _settings["UseProperties"].ToString().ToLower() == "true"; } }
        private bool UsePascalCase { get { return _settings != null && _settings.ContainsKey("UsePascalCase") && _settings["UsePascalCase"].ToString().ToLower() == "true"; } }


        public List<string> ReservedKeywords => new List<string>() { "abstract", "assert", "boolean", "break" ,"byte", "case" ,"catch" ,"char", "class", "const" ,"continue" ,"default" ,"double", "do" ,"else","enum" ,"extends" ,"false" ,"final" ,"finally", "float", "for", "goto", "if", "implements", "import" ,"instanceof" ,"int", "interface" ,"long", "native", "new" ,"null" ,"package", "private","protected" ,"public", "return", "short", "static", "strictfp", "super", "switch" ,"synchronized" ,"this" ,"throw", "throws", "transient" ,"true" ,"try", "void", "volatile", "while" };

        public string Write(IEnumerable<Class> classInfo, string rootName)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var @class in classInfo)
            {
                if (UsePascalCase)
                    @class.Name = @class.Name.ToTitleCase();

                if (!UsePascalCase) // Check for reserved words in case no pascal
                    if (ReservedKeywords.Contains(@class.Name)) @class.Name = "_" + @class.Name;

                sb.AppendFormat("public class {0} {{ {1}", @class.Name, Environment.NewLine);
                foreach (var field in @class.Fields)
                {
                    if (UsePascalCase)
                        field.Name = field.Name.ToTitleCase();

                    if (!UsePascalCase) // Check for reserved words in case no pascal
                        if (ReservedKeywords.Contains(field.Name)) field.Name = "_" + field.Name;

                    // If it's a class property then set the current name of the property as the name of the class
                    var xmltype = field.XmlType == XmlType.Class ? field.Name : GetTypeName(field);

                    // Check if property name starts with number
                    if (!string.IsNullOrEmpty(field.Name) && char.IsDigit(field.Name[0])) field.Name = "_" + field.Name;
                    
                    if (UseProperties)
                    {
                        sb.AppendFormat("\tpublic {0} get{1}() {{ \r\t\t return this.{2}; }} {3}", xmltype, field.Name, field.Name, Environment.NewLine);
                        sb.AppendFormat("\tpublic void set{1}({0} {2}) {{ \r\t\t this.{2} = {2}; }} {3}", xmltype, field.Name, field.Name, Environment.NewLine);
                        sb.AppendFormat("\t{0} {1};", xmltype, field.Name);
                        sb.AppendLine();
                    }
                    else
                    {
                        sb.AppendFormat("\tpublic {0} {1};{2}", xmltype, field.Name, Environment.NewLine);
                    }

                }
                sb.AppendLine("}");
                sb.AppendLine("");
            }

            return sb.ToString();

        }

    }
}

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

        public string Comment => @"";
        private bool UseProperties { get { return _settings != null && _settings.ContainsKey("UseProperties") && _settings["UseProperties"].ToString().ToLower() == "true"; } }

        public List<string> ReservedKeywords => new List<string>() { "abstract", "assert", "boolean", "break" ,"byte", "case" ,"catch" ,"char", "class", "const" ,"continue" ,"default" ,"double", "do" ,"else","enum" ,"extends" ,"false" ,"final" ,"finally", "float", "for", "goto", "if", "implements", "import" ,"instanceof" ,"int", "interface" ,"long", "native", "new" ,"null" ,"package", "private","protected" ,"public", "return", "short", "static", "strictfp", "super", "switch" ,"synchronized" ,"this" ,"throw", "throws", "transient" ,"true" ,"try", "void", "volatile", "while" };

        public string Write(IEnumerable<Class> classInfo, string rootName)
        {
            StringBuilder sb = new StringBuilder();
            // For now check if type is string -> Capitalize it 
            // Later on you make proper type mapping
            foreach (var @class in classInfo)
            {
                sb.AppendFormat("public class {0} {{ {1}", @class.Name, Environment.NewLine);
                foreach (var field in @class.Fields)
                {
                    if (UseProperties)
                        sb.AppendFormat("\tpublic {0} {1}; {2}", field.Type, field.Name, Environment.NewLine);
                    else
                    {
                        // To do remove the "string" condition
                        sb.AppendFormat("\tpublic {0} {1}; {2}", field.Type == "string" ? "String" : field.Type, field.Name, Environment.NewLine);
                    }

                }
                sb.AppendLine("}");
                sb.AppendLine("");
            }

            return sb.ToString();

        }
    }
}

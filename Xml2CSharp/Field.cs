namespace Xml2CSharp
{
    public class Field
    {
        /// <summary>
        /// The main name used when we are writing it in the code writers
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Usually the type of the 
        /// </summary>
        public XmlType InternalXmlType { get; set; }

        /// <summary>
        /// The Xml Type of the class or Array : example : Array
        /// </summary>
        public XmlType XmlType { get; set; }

        /// <summary>
        /// If the attribute is Element or Attribute Type
        /// </summary>
        public XmlAttributeType XmlAttributeType { get; set; }
        public string Namespace { get; set; }

        /// <summary>
        /// The original name as captured from the original XMLJ
        /// </summary>
        public string XmlName { get; set; }

        /// <summary>
        /// This is to mark this field as the Text field of the class in case the class contains a value: example <TEST> FDSAF <OTHER MEMBERS></TEST>
        /// </summary>
        public bool IsValue { get; set; }
    }
}
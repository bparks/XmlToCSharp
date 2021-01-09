using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Xml2CSharp.Clent
{
	[XmlRoot(ElementName = "arrayofint")]
	public class Arrayofint
	{

		[XmlElement(ElementName = "int")]
		public List<int> Int { get; set; }
	}

	[XmlRoot(ElementName = "arrayofstring")]
	public class Arrayofstring
	{

		[XmlElement(ElementName = "string")]
		public List<string> String { get; set; }
	}

	[XmlRoot(ElementName = "position")]
	public class Position
	{

		[XmlElement(ElementName = "internet")]
		public string Internet { get; set; }
	}

	[XmlRoot(ElementName = "arrayofobject")]
	public class Arrayofobject
	{

		[XmlElement(ElementName = "position")]
		public List<Position> Position { get; set; }
	}

	[XmlRoot(ElementName = "realestates")]
	public class Realestates
	{

		[XmlElement(ElementName = "arrayofint")]
		public Arrayofint Arrayofint { get; set; }

		[XmlElement(ElementName = "arrayofstring")]
		public Arrayofstring Arrayofstring { get; set; }

		[XmlElement(ElementName = "arrayofobject")]
		public Arrayofobject Arrayofobject { get; set; }
	}


}

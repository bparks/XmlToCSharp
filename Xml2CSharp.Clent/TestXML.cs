﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Xml2CSharp.Clent
{
	// using System.Xml.Serialization;
	// XmlSerializer serializer = new XmlSerializer(typeof(Realestates));
	// using (StringReader reader = new StringReader(xml))
	// {
	//    var test = (Realestates)serializer.Deserialize(reader);
	// }

	[XmlRoot(ElementName = "externalId")]
	public class ExternalId
	{

		[XmlAttribute(AttributeName = "teststring")]
		public string Teststring { get; set; }

		[XmlAttribute(AttributeName = "testint")]
		public int Testint { get; set; }

		[XmlAttribute(AttributeName = "testbool")]
		public bool Testbool { get; set; }

		[XmlAttribute(AttributeName = "testdouble")]
		public double Testdouble { get; set; }

		[XmlAttribute(AttributeName = "testdatetime")]
		public DateTime Testdatetime { get; set; }

		[XmlAttribute(AttributeName = "testdatetime2")]
		public DateTime Testdatetime2 { get; set; }

		[XmlText]
		public int Text { get; set; }
	}

	[XmlRoot(ElementName = "testclass")]
	public class Testclass
	{

		[XmlElement(ElementName = "testint")]
		public int Testint { get; set; }
	}

	[XmlRoot(ElementName = "realestates")]
	public class Realestates
	{

		[XmlElement(ElementName = "externalId")]
		public ExternalId ExternalId { get; set; }

		[XmlElement(ElementName = "testclass")]
		public Testclass Testclass { get; set; }
	}



}

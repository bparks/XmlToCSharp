using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Xml2CSharp.Clent
{
	[XmlRoot(ElementName = "additionalCosts")]
	public class AdditionalCosts
	{
		[XmlElement(ElementName = "value")]
		public string Value { get; set; }
		[XmlElement(ElementName = "currency")]
		public string Currency { get; set; }
		[XmlElement(ElementName = "marketingType")]
		public string MarketingType { get; set; }
		[XmlElement(ElementName = "priceIntervalType")]
		public string PriceIntervalType { get; set; }
	}

	[XmlRoot(ElementName = "realestates")]
	public class Realestates
	{
		[XmlElement(ElementName = "externalId")]
		public string ExternalId { get; set; }
		[XmlElement(ElementName = "title")]
		public string Title { get; set; }
		[XmlElement(ElementName = "creationDate")]
		public string CreationDate { get; set; }
		[XmlElement(ElementName = "lastModificationDate")]
		public string LastModificationDate { get; set; }
		[XmlElement(ElementName = "thermalCharacteristic")]
		public string ThermalCharacteristic { get; set; }
		[XmlElement(ElementName = "energyConsumptionContainsWarmWater")]
		public string EnergyConsumptionContainsWarmWater { get; set; }
		[XmlElement(ElementName = "buildingEnergyRatingType")]
		public string BuildingEnergyRatingType { get; set; }
		[XmlElement(ElementName = "additionalArea")]
		public string AdditionalArea { get; set; }
		[XmlElement(ElementName = "numberOfFloors")]
		public string NumberOfFloors { get; set; }
		[XmlElement(ElementName = "additionalCosts")]
		public AdditionalCosts AdditionalCosts { get; set; }
	}

}

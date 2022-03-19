

using System.Xml.Serialization;

namespace CarDealer.DataTransferObjects.Output
{
    [XmlType("sale")]
    public class SaleOutputModel
    {
        [XmlElement("car")]
        public CarSalesOutputModel Car { get; set; }

        [XmlElement("discount")]
        public int Discount { get; set; }

        [XmlElement("customer-name")]
        public string CustomerName { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }

        [XmlElement("price-with-discount")]
        public decimal PriceWithDiscount { get; set; }
    }
}
/*
     <discount>30.00</discount>
    <customer-name>Hipolito Lamoreaux</customer-name>
    <price>707.97</price>
    <price-with-discount>495.58</price-with-discount>

 */
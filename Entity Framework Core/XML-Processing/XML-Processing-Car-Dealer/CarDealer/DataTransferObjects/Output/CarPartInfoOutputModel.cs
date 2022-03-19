using System.Xml.Serialization;

namespace CarDealer.DataTransferObjects.Output
{
    [XmlType("part")]
  public  class CarPartInfoOutputModel
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("price")]
        public decimal Price { get; set; }
    }

    /*
     <part name="Master cylinder" price="130.99" />
     */
}

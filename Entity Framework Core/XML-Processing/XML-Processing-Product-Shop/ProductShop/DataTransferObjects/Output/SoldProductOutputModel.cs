using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ProductShop.DataTransferObjects.Output
{
    [XmlType("Product")]
  public  class SoldProductOutputModel
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }
    }
    /*
      <soldProducts>
      <Product>
        <name>olio activ mouthwash</name>
        <price>206.06</price>
      </Product>
     */
}

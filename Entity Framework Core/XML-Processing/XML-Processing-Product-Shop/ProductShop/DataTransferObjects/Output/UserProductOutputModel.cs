using System.Xml.Serialization;

namespace ProductShop.DataTransferObjects.Output
{
    [XmlType("Users")]
    public class UserProductOutputModel
    {
        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("users")]
        public UsersInfoOutputModel[] Users { get; set; }
    }
}

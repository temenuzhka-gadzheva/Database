

using System.Collections.Generic;

namespace RealEstates.Models
{
   public class Property
    {
        public Property()
        {
            this.Tags = new HashSet<Tag>();
        }
        public int Id { get; set; }
        public int Size { get; set; }
        public int? YardSize { get; set; }
        public byte? Floor { get; set; }
        public int? Year { get; set; }
        public byte? TotalFloors { get; set; }

        // Gets or sets the property in Euro
        public int? Price { get; set; }
        public int DistrictId { get; set; }

        // navigational property
        public virtual District District { get; set; }

        public int TypeId { get; set; }
        public virtual PropertyType Type { get; set; }

        public int BuildingTypeId { get; set; }
        public virtual BuildingType BuildingType { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}

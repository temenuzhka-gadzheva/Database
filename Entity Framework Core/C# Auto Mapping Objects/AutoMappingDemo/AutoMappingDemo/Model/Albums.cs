using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoMappingDemo.Model
{
    public partial class Albums
    {
        public Albums()
        {
            Songs = new HashSet<Songs>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int? ProducerId { get; set; }

        [ForeignKey(nameof(ProducerId))]
        [InverseProperty(nameof(Producers.Albums))]
        public virtual Producers Producer { get; set; }
        [InverseProperty("Album")]
        public virtual ICollection<Songs> Songs { get; set; }
    }
}

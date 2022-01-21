using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoMappingDemo.Model
{
    public partial class Album
    {
        public Album()
        {
            Songs = new HashSet<Song>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int? ProducerId { get; set; }

        [ForeignKey(nameof(ProducerId))]
        [InverseProperty(nameof(Model.Producer.Albums))]
        public virtual Producer Producer { get; set; }
        [InverseProperty("Album")]
        public virtual ICollection<Song> Songs { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Models
{
    public partial class Songs
    {
        public Songs()
        {
            SongsPerformers = new HashSet<SongsPerformers>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Genre { get; set; }
        public int? AlbumId { get; set; }
        public int WriterId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [ForeignKey(nameof(AlbumId))]
        [InverseProperty(nameof(Albums.Songs))]
        public virtual Albums Album { get; set; }
        [ForeignKey(nameof(WriterId))]
        [InverseProperty(nameof(Writers.Songs))]
        public virtual Writers Writer { get; set; }
        [InverseProperty("Song")]
        public virtual ICollection<SongsPerformers> SongsPerformers { get; set; }
    }
}

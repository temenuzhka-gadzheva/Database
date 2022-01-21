using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoMappingDemo.Model
{
    public partial class Song
    {
        public Song()
        {
            SongsPerformers = new HashSet<SongsPerformer>();
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

       // public bool IsDeleted { get; set; }

        [ForeignKey(nameof(AlbumId))]
        [InverseProperty(nameof(Model.Album.Songs))]
        public virtual Album Album { get; set; }
        [ForeignKey(nameof(WriterId))]
        [InverseProperty(nameof(Model.Writer.Songs))]
        public virtual Writer Writer { get; set; }
        [InverseProperty("Song")]
        public virtual ICollection<SongsPerformer> SongsPerformers { get; set; }
    }
}

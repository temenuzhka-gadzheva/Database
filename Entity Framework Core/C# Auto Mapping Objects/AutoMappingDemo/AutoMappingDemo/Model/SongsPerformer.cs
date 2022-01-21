using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoMappingDemo.Model
{
    public partial class SongsPerformer
    {
        [Key]
        public int SongId { get; set; }
        [Key]
        public int PerformerId { get; set; }

        [ForeignKey(nameof(PerformerId))]
        [InverseProperty(nameof(Model.Performer.SongsPerformers))]
        public virtual Performer Performer { get; set; }
        [ForeignKey(nameof(SongId))]
        [InverseProperty(nameof(Model.Song.SongsPerformers))]
        public virtual Song Song { get; set; }
    }
}

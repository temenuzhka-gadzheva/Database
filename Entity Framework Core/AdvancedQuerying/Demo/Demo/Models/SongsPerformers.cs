using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Models
{
    public partial class SongsPerformers
    {
        [Key]
        public int SongId { get; set; }
        [Key]
        public int PerformerId { get; set; }

        [ForeignKey(nameof(PerformerId))]
        [InverseProperty(nameof(Performers.SongsPerformers))]
        public virtual Performers Performer { get; set; }
        [ForeignKey(nameof(SongId))]
        [InverseProperty(nameof(Songs.SongsPerformers))]
        public virtual Songs Song { get; set; }
    }
}

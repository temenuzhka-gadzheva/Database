using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoMappingDemo.Model
{
    public partial class Performers
    {
        public Performers()
        {
            SongsPerformers = new HashSet<SongsPerformers>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20)]
        public string LastName { get; set; }
        public int Age { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal NetWorth { get; set; }

        [InverseProperty("Performer")]
        public virtual ICollection<SongsPerformers> SongsPerformers { get; set; }
    }
}

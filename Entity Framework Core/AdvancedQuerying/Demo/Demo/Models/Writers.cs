using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Models
{
    public partial class Writers
    {
        public Writers()
        {
            Songs = new HashSet<Songs>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        public string Pseudonym { get; set; }

        [InverseProperty("Writer")]
        public virtual ICollection<Songs> Songs { get; set; }
    }
}

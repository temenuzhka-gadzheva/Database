using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Models
{
    public partial class Producers
    {
        public Producers()
        {
            Albums = new HashSet<Albums>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        public string Pseudonym { get; set; }
        public string PhoneNumber { get; set; }

        [InverseProperty("Producer")]
        public virtual ICollection<Albums> Albums { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoMappingDemo.Model
{
    public partial class Producer
    {
        public Producer()
        {
            Albums = new HashSet<Album>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        public string Pseudonym { get; set; }
        public string PhoneNumber { get; set; }

        [InverseProperty("Producer")]
        public virtual ICollection<Album> Albums { get; set; }
    }
}

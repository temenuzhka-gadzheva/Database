
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoMappingDemo.Model
{
    public partial class Writer
    {
        public Writer()
        {
            Songs = new HashSet<Song>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pseudonym { get; set; }

       // [InverseProperty("Writer")]
        public virtual ICollection<Song> Songs { get; set; }
    }
}


using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Demo1._1.Data
{
    public class Category
    {
        public Category()
        {
            this.News = new HashSet<News>();
        }
        [Key]
        public int Id { get; set; }
       
        [MaxLength(150)]
        public string Title { get; set; }
        public virtual ICollection<News> News { get; set; }
    }
}

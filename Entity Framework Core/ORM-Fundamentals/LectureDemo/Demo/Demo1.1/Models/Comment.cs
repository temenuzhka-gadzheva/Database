using System.ComponentModel.DataAnnotations;


namespace Demo1._1.Data
{
   public class Comment
    {
        [Key]
        public int Id { get; set; }

        public int NewsId { get; set; }
        public virtual News News { get; set; }
        [MaxLength(50)]
        public string  Author { get; set; }
        [MaxLength(4000)]
        public string Content { get; set; }
    }
}

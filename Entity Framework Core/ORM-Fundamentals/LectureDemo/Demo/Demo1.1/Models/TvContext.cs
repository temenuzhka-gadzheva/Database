using Microsoft.EntityFrameworkCore;

namespace Demo1._1.Data
{
   public  class TvContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Integrated Security=true;Database=TvDB");
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}

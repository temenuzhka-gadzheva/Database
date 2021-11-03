using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstDemo.Models
{
    public class SlidoDbContext: DbContext
    {
        public SlidoDbContext()
        {

        }

        public SlidoDbContext(DbContextOptions dbContextOptions) 
            : base(dbContextOptions)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                .Property(x => x.Content)
                .IsUnicode(false);
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.
                    UseSqlServer("Server=.;Database=SlidoDb;Integrated Security=true");

            }
        }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Question> Questions { get; set; }
    }
}

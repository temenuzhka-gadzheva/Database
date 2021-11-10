using EntityRelationsDemo.ModelBuilding;
using EntityRelationsDemo.Models;
using Microsoft.EntityFrameworkCore;


namespace EntityRelationsDemo
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions options)
        :base(options)
        {

        }
      
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<EmployeeInClub> EmployeeInClubs { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer($"Server=.;Database=EfCoreDemo;Integrated Security=true;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // department

            modelBuilder.Entity<Department>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(40);

            // employee configuration
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());

            /*modelBuilder.Entity<Address>()
                .HasOne(x => x.Employee)
                .WithOne(x => x.Address);*/

            // relation 1 - many
            /* modelBuilder.Entity<Employee>()
                 .HasOne(x => x.Department)
                 .WithMany(x => x.Employees)
                 .HasForeignKey(x => x.DepartmentId)
                 .OnDelete(DeleteBehavior.Cascade);*/

            // realtion many to many
            // can make   directly  realtion clubs to employees with ICollection without another information 
            modelBuilder.Entity<EmployeeInClub>()
                .HasKey(x => new { x.EmployeeId, x.ClubId });

            base.OnModelCreating(modelBuilder);
        }
    }
}

using EntityRelationsDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityRelationsDemo.ModelBuilding
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            // realtion one-one 
           

            // renaming table from employees to people with schema company
            builder
                 .ToTable("People", "company");

            // renaming property name
            builder.Property(x => x.StartWorkDate)
                   .HasColumnName("StartedOn");

            // renaming property name and column type
            // avoid this because should different databases and type should have not 
            builder.Property(x => x.StartWorkDate)
                 .HasColumnName("StartedOn")
                 .HasColumnType("DATE");

            // setting to primary key if have not
            builder.HasKey(x => x.Id);

            // to create compositive key 
            builder.Property(x => x.Egn).IsRequired();
            builder.HasKey(x => new { x.Id, x.Egn });

            // first and last name not to be null
            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();

            // constraint to 20 lenght
            builder.Property(x => x.FirstName).HasMaxLength(20);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(20);

            // to ignore salary
          //  builder.Property(x => x.Salary).ValueGeneratedOnAddOrUpdate();

            // ingnore column
            builder.Ignore(x => x.FullName);

            builder.HasOne(x => x.Department) // required
                   .WithMany(x => x.Employees) // optional (inverse prorepty)
                   .HasForeignKey(x => x.DepartmentId) // db column name (optional)
                   .OnDelete(DeleteBehavior.Restrict); // no cascade delete 
          
            // salary precision
            builder.Property(x => x.Salary).HasPrecision(12,3);
        }
    }
}

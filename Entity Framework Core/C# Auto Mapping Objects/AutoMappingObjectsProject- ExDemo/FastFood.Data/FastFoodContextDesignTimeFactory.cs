using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace FastFood.Data
{
    public class FastFoodContextDesignTimeFactory : IDesignTimeDbContextFactory<FastFoodContext>
    {
        public FastFoodContext CreateDbContext(string[] args)
        { // second variant 

          /*  var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var builder = new DbContextOptionsBuilder<FastFoodContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
          */

            // first variant
             var builder = new DbContextOptionsBuilder<FastFoodContext>();
             builder.UseSqlServer("Server=.;Database=FastFood;Trusted_Connection=True;MultipleActiveResultSets=true");


            return new FastFoodContext(builder.Options);
        }
    }
}

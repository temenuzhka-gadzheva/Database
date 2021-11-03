using CodeFirstDemo.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CodeFirstDemo
{
    class StartUp
    {
        static void Main()
        {
            var db = new SlidoDbContext();
            db.Database.Migrate();
        }
    }
}

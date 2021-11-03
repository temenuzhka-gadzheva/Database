using EntityFrameworkCoreIntroductionDemo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DatabaseFistDemo

{
    class Program
    {
        static void Main()
        {
            var db = new SoftUniContext();


        }


        // if I can connect to different database 
        // settings
        private static void ConnectToDifferentDatabase()
        {
            var options = new DbContextOptionsBuilder<SoftUniContext>()
.UseSqlServer("Server=.;Database=Movies;Integrated Security=True");
            var db = new SoftUniContext(options.Options);
        }

        // method to query string
        private static void UseQueryToString(SoftUniContext db)
        {
            var employee = db.Employees.Select(x => new
            {
                x.FirstName,
                DepartmentsCoutn = x.Departments.Count(),
                FirstProjectName = x.EmployeesProjects.FirstOrDefault().Project.Name
            });
            Console.WriteLine(employee.ToQueryString());
        }

        private static void SearchDemo(SoftUniContext db)
        {
            var searchNames = db.Employees.
                Where(x => x.EmployeesProjects.Any(e => e.Employee.FirstName == "Barry"));
            Console.WriteLine(db.Employees.Count());
            Console.WriteLine(db.Projects.Count());
            foreach (var searchName in searchNames)
            {
                Console.WriteLine($"{searchName.FirstName} {searchName.JobTitle} {searchName.Salary}");
            }
        }

        // delete employee
        private static void DeleteEmployee(SoftUniContext db)
        {
            // first way
            var employee = db.Employees.OrderBy(x => x.EmployeeId)
                    .FirstOrDefault();
            db.Employees.Remove(employee);
            db.SaveChanges();
            //second way
            //if know Id of object
            var emp = new Employee { EmployeeId = 1 };
            db.Employees.Remove(emp);
            db.SaveChanges();

        }

        // update name of employee
        private static void UpdateEmplyeeName(SoftUniContext db)
        {
            var employee = db.Employees.
                OrderBy(x => x.EmployeeId).
                FirstOrDefault();
            employee.FirstName = "Gui";
            db.SaveChanges();
        }
    }
}

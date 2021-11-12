using DemoLinq.Models;
using System;
using System.Linq;

namespace DemoLinq
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var db = new SoftUniContext();
            var employees = db.Employees
                .Where(x => x.FirstName.StartsWith("P"))
                .ToList();
            //  Console.WriteLine(employees.Count());
            foreach (var emp in employees)
            {
                Console.WriteLine(emp.FirstName);
            }

            var departments = db.Departments
                .Where(x => x.ManagerId == 2).ToList();

            Console.WriteLine(departments.Count());
        }
    }
}

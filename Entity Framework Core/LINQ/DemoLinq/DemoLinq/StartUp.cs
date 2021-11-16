using DemoLinq.Models;
using System;
using System.Linq;

namespace DemoLinq
{
    public class StartUp
    {
        public static void Main()
        {
            var db = new SoftUniContext();

              var employees = db.Employees
               .Where(x => x.FirstName.StartsWith("P"))
               .Max(x => x.FirstName)
               .ToList();

              var employees2 = db.Employees
                .OrderBy(x => x.JobTitle)
                .Where(x => x.JobTitle == "Stocker")
                .Select(x => new { x.FirstName, Job = x.JobTitle })
                .ToList();

            // inner join
              var employees3 = db.Employees
                  .Join(db.EmployeesProjects,
                        x => x.EmployeeId,
                        x => x.EmployeeId, (employee, project) => new
                        {
                            EmployeeName = employee.FirstName,
                            ProjectName = project.Project.Name

                        })
                    .ToList();

            // group by
            var towns = db.Towns
                .GroupBy(x => x.Name.Substring(0, 1))
                .Select(x => new
                {
                    FirstLetter = x.Key,
                    Count = x.Count(),
                    Min = x.Min(t => t.Name),
                    Max = x.Max(t => t.Name)
                })
                .ToList();

            // select many

            var emps = db.Employees
                .SelectMany(x => x.EmployeesProjects
                               .Select(ep => x.FirstName + " => " +
                               ep.Employee.FirstName))
                .ToList();

            /*  foreach (var town in towns)
              {
                  Console.WriteLine(town);
              }*/

            //   Console.WriteLine(employees.Count());

            /*   foreach (var emp in employees)
               {
                   Console.WriteLine(emp);
               }*/

            var departments = db.Departments.Where(x => x.Employees.Count() > 1)
                .OrderBy(x => x.Name)
                .Select(x => new 
                {
                  DepartmentName =   x.Name,
                  ManagerName = x.Employees.FirstOrDefault().Manager.FirstName
                })
                .Distinct()
                .ToList();

            foreach (var dep in departments)
            {
                if (dep.ManagerName == null)
                {
                    Console.WriteLine($"{dep.DepartmentName} => have not manager");
                }
                Console.WriteLine($"{dep.DepartmentName} => {dep.ManagerName}");
            }
       





        }
    }
}

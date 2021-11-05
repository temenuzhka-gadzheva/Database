using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using SoftUni.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        public static void Main()
        {
            var context = new SoftUniContext();
         
        }


        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            var employees = context.Employees.
                 OrderBy(x => x.EmployeeId).ToList();

            var sb = new StringBuilder();
            foreach (var employee in employees)
            {

                sb.AppendLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary:F2}");
            }
            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            var employees = context.Employees.
                OrderBy(x => x.FirstName)
                .Where(x => x.Salary > 50000).ToList();

            var sb = new StringBuilder();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} - {employee.Salary:F2}");
            }
            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            var employees = context.Employees.
                Where(x => x.Department.Name == "Research and Development")
              .Select(x => new
              {
                  x.FirstName,
                  x.LastName,
                  x.Salary,
                  DepartmentName = x.Department.Name
              })
                .OrderBy(x => x.Salary)
               .ThenByDescending(x => x.FirstName).ToList();

            var sb = new StringBuilder();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} from {employee.DepartmentName} - ${employee.Salary:F2}");
            }
            return sb.ToString().TrimEnd();
        }

        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            var employeeNakov = context.Employees.
                FirstOrDefault(x => x.LastName == "Nakov");
            employeeNakov.Address = new Address
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            context.SaveChanges();

            var employees = context.Employees
                .Select(x => new
                {
                    AddressText = x.Address.AddressText,
                    x.Address.AddressId
                })
                .OrderByDescending(x => x.AddressId)
                .Take(10)
                .ToList();

            var sb = new StringBuilder();

            foreach (var item in employees)
            {
                sb.AppendLine($"{item.AddressText}");
            }
            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            var firstTenEmployees = context.Employees
                .Include(x => x.EmployeesProjects)
                .ThenInclude(x => x.Project)
                .Where(x => x.EmployeesProjects
                .Any(p => p.Project.StartDate.Year >= 2001 &&
                            p.Project.StartDate.Year <= 2003))
                .Select(x => new
                {
                    eFirstName = x.FirstName,
                    eLastName = x.LastName,
                    mFirstName = x.Manager.FirstName,
                    mLastName = x.Manager.LastName,
                    Projects = x.EmployeesProjects.Select(p => new
                    {
                        ProjectName = p.Project.Name,
                        StartDate = p.Project.StartDate,
                        EndDate = p.Project.EndDate
                    })
                })
                .Take(10)
                .ToList();

            var sb = new StringBuilder();

            foreach (var employee in firstTenEmployees)
            {
                sb.AppendLine($"{employee.eFirstName} {employee.eLastName} - Manager: {employee.mFirstName} {employee.mLastName}");

                // to get all projects
                foreach (var project in employee.Projects)
                {

                    var endDate = project.
                                EndDate.HasValue ?
                                project.EndDate.Value.
                                ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)
                                : "not finished";

                    var startDate = project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                    var projectName = project.ProjectName;

                    sb.AppendLine($"--{projectName} - {startDate} - {endDate}");

                }
            }
            return sb.ToString().TrimEnd();

        }

        public static string GetAddressesByTown(SoftUniContext context)
        {
            var addresses = context.Addresses
               .OrderByDescending(x => x.Employees.Count)
               .ThenBy(x => x.Town.Name)
               .ThenBy(x => x.AddressText)
                .Select(x => new
                {
                    addressText = x.AddressText,
                    townName = x.Town.Name,
                    empCount = x.Employees.Count,

                })
               .Take(10)
               .ToList();
            var sb = new StringBuilder();

            foreach (var address in addresses)
            {
                sb.AppendLine($"{address.addressText}, {address.townName} - {address.empCount} employees");
            }
            return sb.ToString().TrimEnd();
        }

        public static string GetEmployee147(SoftUniContext context)
        {
            var employeeWith147Id = context.Employees
                .Select(x => new
                {
                    Id = x.EmployeeId,
                    Fname = x.FirstName,
                    Lname = x.LastName,
                    Job = x.JobTitle,
                    Projects = x.EmployeesProjects.
                    Select(p => new
                    {
                        pName = p.Project.Name,

                    }).
                    OrderBy(p => p.pName)
                    .ToList()
                })
                .Where(x => x.Id == 147)
                .ToList();

            var sb = new StringBuilder();
            foreach (var emp in employeeWith147Id)
            {
                sb.AppendLine($"{emp.Fname} {emp.Lname} - {emp.Job}");

                foreach (var project in emp.Projects)
                {
                    sb.AppendLine($"{project.pName}");
                }
            }
            return sb.ToString().TrimEnd();
        }

        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            var departments = context.Departments
                  .Where(x => x.Employees.Count > 5)
                .OrderBy(x => x.Employees.Count)
                .ThenBy(x => x.Name)
                .Select(x => new
                {
                    depName = x.Name,
                    mFname = x.Manager.FirstName,
                    mLname = x.Manager.LastName,
                    Emps = x.Employees.
                    Select(e => new
                    {
                        eFname = e.FirstName,
                        eLname = e.LastName,
                        eJob = e.JobTitle
                    })
                   .OrderBy(e => e.eFname)
                   .ThenBy(e => e.eLname)
                   .ToList()
                })
                .ToList();

            var sb = new StringBuilder();

            foreach (var department in departments)
            {
                sb.AppendLine($"{department.depName} - {department.mFname} {department.mLname}");

                foreach (var employee in department.Emps)
                {
                    sb.AppendLine($"{employee.eFname} {employee.eLname} - {employee.eJob}");
                }
            }
            return sb.ToString().TrimEnd();
        }

        public static string GetLatestProjects(SoftUniContext context)
        {

            var projects = context.Projects
                 .OrderByDescending(x => x.StartDate)
                .Take(10)
                .OrderBy(x => x.Name)
                .Select(x => new
                {
                    pName = x.Name,
                    pDescription = x.Description,
                    pStartDate = x.StartDate
                })
                .ToList();


            var sb = new StringBuilder();

            foreach (var project in projects)
            {
                var startDate = project.pStartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                sb.AppendLine($"{project.pName}");
                sb.AppendLine($"{project.pDescription}");
                sb.AppendLine($"{startDate}");
            }
            return sb.ToString().TrimEnd();
        }

        public static string IncreaseSalaries(SoftUniContext context)
        {
            var selectedDepartments = new List<string>
            {
                 "Engineering",
                 "Tool Design",
                 "Marketing",
                 "Information Services"
            };

            var employeesByDepartments = context.Employees
                .Where(x => selectedDepartments.Contains(x.Department.Name))
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ToList();
            var sb = new StringBuilder();
            foreach (var employee in employeesByDepartments)
            {
                employee.Salary *= 1.12m;
                context.SaveChanges();
                sb.AppendLine($"{employee.FirstName} {employee.LastName} (${employee.Salary:F2})");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            var employees = context.Employees
                .Where(x => x.FirstName.StartsWith("Sa"))
                .Select(x => new
                {
                    fName = x.FirstName,
                    lName = x.LastName,
                    job = x.JobTitle,
                    salary = x.Salary
                })
                .OrderBy(x => x.fName)
                .ThenBy(x => x.lName)
                .ToList();

            var sb = new StringBuilder();
            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.fName} {employee.lName} - {employee.job} - (${employee.salary:F2})");
            }

            return sb.ToString().TrimEnd();
        }
        public static string DeleteProjectById(SoftUniContext context)
        {
            var projectWithId2 = context.Projects
                .FirstOrDefault(x => x.ProjectId == 2);

            var employeeProject = context.EmployeesProjects
                .Where(x => x.EmployeeId == 2)
                .ToList();

            foreach (var project in employeeProject)
            {
                context.EmployeesProjects.Remove(project);
            }
            context.Projects.Remove(projectWithId2);
            context.SaveChanges();

            var sb = new StringBuilder();

            var projects = context.Projects
                .Select(x => x.Name)
                .Take(10)
                .ToList();

            foreach (var project in projects)
            {
                sb.AppendLine($"{project}");
            }

            return sb.ToString().TrimEnd();

        }

        public static string RemoveTown(SoftUniContext context)
        {
            var seattleToRemove = context.Towns
                .FirstOrDefault(x => x.Name == "Seattle");

            var addressesToRemove = context.Addresses
                .Where(a => a.TownId == seattleToRemove.TownId);

            var employeesToRemove = context.Employees
                .Where(e => addressesToRemove
                               .Any(a => a.AddressId == e.AddressId));

            var removedCount = addressesToRemove.Count();

            foreach (var employee in employeesToRemove)
            {
                employee.AddressId = null;
            }

            foreach (var address in addressesToRemove)
            {
                context.Addresses.Remove(address);
            }

            context.Towns.Remove(seattleToRemove);
            context.SaveChanges();

            return $"{removedCount} addresses in Seattle were deleted";
        }
    }
}

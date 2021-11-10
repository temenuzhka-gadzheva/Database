using System;
using System.Collections.Generic;
using System.Text;

namespace EntityRelationsDemo.Models
{
    public class Department
    {
        public Department()
        {
            this.Employees = new HashSet<Employee>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        // collection to all employees
        public ICollection<Employee> Employees { get; set; }
    }
}

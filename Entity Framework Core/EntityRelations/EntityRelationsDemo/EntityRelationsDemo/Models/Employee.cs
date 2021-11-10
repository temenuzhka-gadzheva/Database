using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EntityRelationsDemo.Models
{
    // to rename table 
    [Table("People", Schema = "company")]
    // to search
    [Index("Egn", IsUnique = true)]
    public class Employee
    {

        public Employee()
        {
            this.EmployeeInClubs = new HashSet<EmployeeInClub>();
        }
        public int Id { get; set; }
        public string Egn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
       
        //rename column name
        [Column("StartedOn", TypeName = "date")]
        public DateTime? StartWorkDate { get; set; }
        public decimal Salary { get; set; }

        // composition relation to department
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public ICollection<EmployeeInClub> EmployeeInClubs { get; set; }
        // relation one-one
      /* [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public Address Address { get; set; }*/
    }
}

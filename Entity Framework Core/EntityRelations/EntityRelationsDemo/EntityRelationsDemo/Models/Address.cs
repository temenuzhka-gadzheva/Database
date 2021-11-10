using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EntityRelationsDemo.Models
{ // relation one-one
   public class Address
    {
        public int Id { get; set; }

      /*  [ForeignKey("Employee")]*/
        public int? EmployeeId { get; set; }

        public Employee Employee { get; set; }

    }
}

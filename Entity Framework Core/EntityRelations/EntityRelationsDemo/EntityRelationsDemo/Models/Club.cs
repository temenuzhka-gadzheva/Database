using System;
using System.Collections.Generic;
using System.Text;

namespace EntityRelationsDemo.Models
{
    public class Club
    {
        public Club()
        {
            this.EmployeeInClubs = new HashSet<EmployeeInClub>();
        }
        public int Id { get; set; }

        public string Name { get; set; }
        public ICollection<EmployeeInClub> EmployeeInClubs { get; set; }
    }

}

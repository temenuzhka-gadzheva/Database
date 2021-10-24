using System;
using System.Collections.Generic;

#nullable disable

namespace Demo1._2.Models
{
    public partial class Customer
    {
        public int Id { get; set; }
        public long AccountNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmergencyName { get; set; }
        public string EmergencyNumber { get; set; }
        public string Notes { get; set; }
    }
}

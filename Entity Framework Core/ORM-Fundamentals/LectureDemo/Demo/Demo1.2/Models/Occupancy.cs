using System;
using System.Collections.Generic;

#nullable disable

namespace Demo1._2.Models
{
    public partial class Occupancy
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime? DateOccupied { get; set; }
        public long? AccountNumber { get; set; }
        public int RoomNumber { get; set; }
        public decimal? RateApplied { get; set; }
        public decimal? PhoneCharge { get; set; }
        public string Notes { get; set; }
    }
}

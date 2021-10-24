using System;
using System.Collections.Generic;

#nullable disable

namespace Demo1._2.Models
{
    public partial class Payment
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime PaymentDate { get; set; }
        public long AccountNumber { get; set; }
        public DateTime FirstDateOccupied { get; set; }
        public DateTime LastDateOccupied { get; set; }
        public int? TotalDays { get; set; }
        public decimal? AmountCharged { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal PaymentTotal { get; set; }
        public string Notes { get; set; }
    }
}

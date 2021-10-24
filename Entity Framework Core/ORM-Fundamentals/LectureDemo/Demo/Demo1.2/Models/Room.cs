using System;
using System.Collections.Generic;

#nullable disable

namespace Demo1._2.Models
{
    public partial class Room
    {
        public int RoomNumber { get; set; }
        public string RoomType { get; set; }
        public string BedType { get; set; }
        public decimal? Rate { get; set; }
        public string RoomStatus { get; set; }
        public string Notes { get; set; }
    }
}

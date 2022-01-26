using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonDemo
{
   public class Car
   {
        public Car()
        {
            this.Extras = new List<string>();
        }
        public string Vendor { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public DateTime ManufacturedOn { get; set; }
        public List<string> Extras { get; set; }
        public Engine Engine { get; set; }
    }
}

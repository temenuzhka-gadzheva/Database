using Newtonsoft.Json;
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
        // order of properties
        // changing of property of name
        // how to cope with null values
        [JsonProperty(Order = 10, PropertyName ="test", NullValueHandling = NullValueHandling.Ignore)]
        public string Vendor { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public DateTime ManufacturedOn { get; set; }
        // ignore in json format
        // use Newtonsoft Json using
        [JsonIgnore]
        public List<string> Extras { get; set; }
        public Engine Engine { get; set; }
    }
}

using CsvHelper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml;
/*using System.Text.Json;
using System.Text.Json.Serialization;*/

namespace JsonDemo
{
    public class Program
    {
        public static void Main()
        {
            /*var car = new Car
            {
                Extras = new List<string> { "Klimatronik", "4x4", "Farove" },
                ManufacturedOn = DateTime.Now,
                Model = "Audi",
                Vendor = "Audi",
                Price = 78000.00M,
                Engine = new Engine { HorsePower = 333, Volume = 2.3F }
            };*/

            // Console.WriteLine(JsonConvert.SerializeObject(car, Formatting.Indented));
            //--------------------------------------------------------------------------------

            // using syste.Text.Json
            // File.WriteAllText("myCar.json",JsonSerializer.Serialize(car));
            // to order data 
            // var options = new JsonSerializerOptions { WriteIndented = true };
            //  Console.WriteLine(JsonSerializer.Serialize(car, options));

            // read for small files
            /* var json = File.ReadAllText("myCar.json");
             var carFromJson = JsonSerializer.Deserialize<Car>(json);*/

            // using Newtonsoft.Json
            /* var json = File.ReadAllText("myCar.json");

             Console.WriteLine(JsonConvert.SerializeObject(new { Name = "Marty", Course = "EFCore"}));

             var carFromJson = JsonConvert.DeserializeObject<Car>(json);*/

            //------------------------------------------------------------------------------------

            // json work with anonymous types
            // Console.WriteLine(JsonConvert.SerializeObject(new { Car = car, Name = "Marty"}, Formatting.Indented));
            //---------------------------------------------------------------------------------------------------------

            // settings of json
            //  naming of properties
            //  date formatting
            //  titming zones
            /*  var settings = new JsonSerializerSettings
              {
                  Formatting = Formatting.Indented,
                  ContractResolver = new DefaultContractResolver
                  {
                      NamingStrategy = new CamelCaseNamingStrategy()
                  },
                  DateFormatString = "yyyy-MM-dd",
                  DateTimeZoneHandling = DateTimeZoneHandling.Utc

              };*/
            //  Console.WriteLine(JsonConvert.SerializeObject(car,settings));
            //------------------------------------------------------------------



            //  linq to json  & json to linq
            /*var json = File.ReadAllText("myCar.json");
            var jObject =  JObject.Parse(json);
            // jObject.Children().Where(x => x)

            foreach (var item in jObject.Children().Where(x => x.Children().Count() > 0))
            {
                Console.WriteLine(item);
                Console.WriteLine(new string('-', 60));
            }
            var car = JsonConvert.DeserializeObject<Car>(json);*/
            //-----------------------------------------------------------------------------------

            // json to xml & xml to json
            // can root object
            // var json = File.ReadAllText("myCar.json");
            //  var xmlDoc = JsonConvert.DeserializeXmlNode(json);

            // xml to json
            /* var xml = new XmlDocument();
             var root = xml.AppendChild(xml.CreateElement("test", "testov"));
             xml.AppendChild(root);
             root.AppendChild(xml.CreateElement("pesho", "peshev"));
             root.AppendChild(xml.CreateElement("admin", "admin"));

             Console.WriteLine(JsonConvert.SerializeXmlNode(xml, Newtonsoft.Json.Formatting.Indented));*/

            //------------------------------------------------------------------------------------------------------

            // with edit  paste special we can convert json (with double "" "") as classes
            // for example
            /* var obj = JsonConvert.DeserializeObject<Rootobject>(@"{""web-app"": {
               ""servlet"": [
                   {
                      ""servlet - name"": ""cofaxCDS"",
                       ""servlet -class"": ""org.cofax.cds.CDSServlet""
                   }
                           ]
              }");*/


            //------------------------------------------------------------------------------------------------------

            // format csv
            // install csv helper
            // generate class
            using (var reader = new StreamReader("Departments.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var record = csv.GetRecords<Department>();
            }

        }
        class Department
        {
            public int DepartmentId { get; set; }
            public string Name { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public decimal Salary { get; set; }
        }
    }

}

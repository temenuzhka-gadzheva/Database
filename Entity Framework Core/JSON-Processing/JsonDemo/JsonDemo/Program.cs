using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
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

            // json work with anonymous types
            // Console.WriteLine(JsonConvert.SerializeObject(new { Car = car, Name = "Marty"}, Formatting.Indented));

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

              };
              Console.WriteLine(JsonConvert.SerializeObject(car,settings));

              */


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


            // json to xml & xml to json
            // can root object
            var json = File.ReadAllText("myCar.json");
             var xmlDoc = JsonConvert.DeserializeXmlNode(json);
            // xml to 

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

            /* var obj = JsonConvert.DeserializeObject<Rootobject>(@"{""web-app"": {
     ""servlet"": [
       {
                 ""servlet - name"": ""cofaxCDS"",
         ""servlet -class"": ""org.cofax.cds.CDSServlet"",
         ""init-param"": {
           ""configGlossary:installationAt"": ""Philadelphia, PA"",
           ""configGlossary:adminEmail"": ""ksm @pobox.com"",
           ""configGlossary:poweredBy"": ""Cofax"",
           ""configGlossary:poweredByIcon"": ""/images/cofax.gif"",
           ""configGlossary:staticPath"": ""/content/static"",
           ""templateProcessorClass"": ""org.cofax.WysiwygTemplate"",
           ""templateLoaderClass"": ""org.cofax.FilesTemplateLoader"",
           ""templatePath"": ""templates"",
           ""templateOverridePath"": """",
           ""defaultListTemplate"": ""listTemplate.htm"",
           ""defaultFileTemplate"": ""articleTemplate.htm"",
           ""useJSP"": false,
           ""jspListTemplate"": ""listTemplate.jsp"",
           ""jspFileTemplate"": ""articleTemplate.jsp"",
           """"cachePackageTagsTrack"""": 200,
           ""cachePackageTagsStore"": 200,
           ""cachePackageTagsRefresh"": 60,
           ""cacheTemplatesTrack"": 100,
           ""cacheTemplatesStore"": 50,
           ""cacheTemplatesRefresh"": 15,
           ""cachePagesTrack"": 200,
           ""cachePagesStore"": 100,
           ""cachePagesRefresh"": 10,
           ""cachePagesDirtyRead"": 10,
           ""searchEngineListTemplate"": ""forSearchEnginesList.htm"",
           ""searchEngineFileTemplate"": ""forSearchEngines.htm"",
           ""searchEngineRobotsDb"": ""WEB-INF/robots.db"",
           ""useDataStore"": true,
           ""dataStoreClass"": ""org.cofax.SqlDataStore"",
           ""redirectionClass"": ""org.cofax.SqlRedirection"",
           ""dataStoreName"": ""cofax"",
           ""dataStoreDriver"": ""com.microsoft.jdbc.sqlserver.SQLServerDriver"",
           ""dataStoreUrl"": ""jdbc:microsoft:sqlserver://LOCALHOST:1433;DatabaseName=goon"",
           ""dataStoreUser"": ""sa"",
           ""dataStorePassword"": ""dataStoreTestQuery"",
           ""dataStoreTestQuery"": ""SET NOCOUNT ON;select test = 'test';"",
           ""dataStoreLogFile"": ""/usr/local/tomcat/logs/datastore.log"",
           ""dataStoreInitConns"": 10,
           ""dataStoreMaxConns"": 100,
           ""dataStoreConnUsageLimit"": 100,
           ""dataStoreLogLevel"": ""debug"",
           ""maxUrlLength"": 500}
 },
       {
     ""servlet - name"": ""cofaxEmail"",
         ""servlet -class"": ""org.cofax.cds.EmailServlet"",
         ""init - param"": {
     ""mailHost"": ""mail1"",
         ""mailHostOverride"": ""mail2""}},
       {
     ""servlet - name"": ""cofaxAdmin"",
         ""servlet -class"": ""org.cofax.cds.AdminServlet""},

       {
     ""servlet - name"": ""fileServlet"",
         ""servlet -class"": ""org.cofax.cds.FileServlet""},
       {
     ""servlet - name"": ""cofaxTools"",
         ""servlet -class"": ""org.cofax.cms.CofaxToolsServlet"",
         ""init - param"": {
     ""templatePath"": ""toolstemplates / "",
           ""log"": 1,
           ""logLocation"": "" / usr / local / tomcat / logs / CofaxTools.log"",
           ""logMaxSize"": """",
           ""dataLog"": 1,
           ""dataLogLocation"": "" / usr / local / tomcat / logs / dataLog.log"",
           ""dataLogMaxSize"": """",
           ""removePageCache"": "" / content / admin / remove ? cache = pages & id = "",
           ""removeTemplateCache"": "" / content / admin / remove ? cache = templates & id = "",
           ""fileTransferFolder"": "" / usr / local / tomcat / webapps / content / fileTransferFolder"",
           ""lookInContext"": 1,
           ""adminGroupID"": 4,
           ""betaServer"": true}}],
     ""servlet - mapping"": {
     ""cofaxCDS"": "" / "",
       ""cofaxEmail"": "" / cofaxutil / aemail/*"",
       ""cofaxAdmin"": ""/admin/*"",
       ""fileServlet"": ""/static/*"",
       ""cofaxTools"": ""/tools/*""},

     ""taglib"": {
       ""taglib-uri"": ""cofax.tld"",
       ""taglib-location"": ""/WEB-INF/tlds/cofax.tld""}}}");

            */


        }
    }

}

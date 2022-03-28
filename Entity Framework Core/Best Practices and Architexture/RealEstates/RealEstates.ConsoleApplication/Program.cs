using Microsoft.EntityFrameworkCore;
using RealEstates.Data;
using RealEstates.Services;
using System;
using System.Text;

namespace RealEstates.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            var db = new ApplicationDbContext();
            // to create migrations 
            // create when started app
            db.Database.Migrate();

            // create menu
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Property search");
                Console.WriteLine("2. Most expensive districts");
                Console.WriteLine("3. Average price per square meter");
                Console.WriteLine("0. Exit");

                bool parsed = int.TryParse(Console.ReadLine(), out int option);

                if (parsed && option == 0)
                {
                    break;
                }
                if (parsed && option >= 1 && option <= 3)
                {
                    switch (option)
                    {
                        case 1:
                            PropertySearch(db);
                            break;
                        case 2:
                            MostExpensiveDistricts(db);
                            break;
                        case 3:
                            AveragePricePerSquareMeter(db);
                            break;
                    }
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        private static void AveragePricePerSquareMeter(ApplicationDbContext context)
        {
            IPropertiesService propertiesService = new PropertiesService(context);
            Console.WriteLine($"Average price per square meter: {propertiesService.AveragePricePerSquareMeter():0.00}€/m²");
        }
        private static void MostExpensiveDistricts(ApplicationDbContext context)
        {
            Console.Write("Districts count:");
            int count = int.Parse(Console.ReadLine());

            IDistrictsService service = new DistrictsService(context);

            var districts = service.GetMostExpensiveDistricts(count);

            foreach (var item in districts)
            {
                Console.WriteLine($"{item.Name} => {item.AveragePricePerSquareMeter:0.00}€/m² ({item.PropertiesCount})");
            }
        }

        private static void PropertySearch(ApplicationDbContext context)
        {
            Console.Write("Min price:");
            int minPrice = int.Parse(Console.ReadLine());

            Console.Write("Max price:");
            int maxPrice = int.Parse(Console.ReadLine());

            Console.Write("Min size:");
            int minSize = int.Parse(Console.ReadLine());

            Console.Write("Max size:");
            int maxSize = int.Parse(Console.ReadLine());

            IPropertiesService service = new PropertiesService(context);

            var properties = service.Search(minPrice, maxPrice, minSize, maxSize);
            foreach (var item in properties)
            {
                Console.WriteLine($"{item.DistrictName}; {item.BuildingType}; {item.PropertyType} => {item.Price}€ => {item.Size}m²");
            }
        }
    }
}

using CarDealer.Data;
using CarDealer.DataTransferObjects.Input;
using CarDealer.Models;
using CarDealer.XmlHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace CarDealer
{
    public class StartUp
    {
       
        public static void Main(string[] args)
        {
            var context = new CarDealerContext();
          // context.Database.EnsureDeleted();
          //  context.Database.EnsureCreated();

            // right button ->  properties -> copy always 
          //  var supplierXml = File.ReadAllText("./Datasets/suppliers.xml");
          //  var partsXml = File.ReadAllText("./Datasets/parts.xml");
          //  var carsXml = File.ReadAllText("./Datasets/cars.xml");
           // var customerXml = File.ReadAllText("./Datasets/customers.xml");
            var salesXml = File.ReadAllText("./Datasets/sales.xml");

            // var result = ImportSuppliers(context, supplierXml);
            // var result = ImportParts(context, partsXml);
            // var result = ImportCars(context, carsXml);
           // var result = ImportCustomers(context, customerXml);
            var result = ImportSales(context, salesXml);
            Console.WriteLine(result);
        }

        // Import Suppliers
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            const string root = "Suppliers";
            var suppliersDto = XmlConverter.
                Deserializer<SupplierInputModel>(inputXml, root);

            var suppliers = suppliersDto.Select(x => new Supplier
            {
                Name = x.Name,
                IsImporter = x.IsImporter
            })
            .ToList();

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Count()}";
        }

        // Import Parts
        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            const string root = "Parts";
            var partsDto = XmlConverter.
                 Deserializer<PartInputModel>(inputXml, root);

            var suppliersIds = context.Suppliers.Select(x => x.Id).ToList();

            var parts = partsDto.Where(x => suppliersIds.Contains(x.SupplierId)).Select(x => new Part
            {
                Name = x.Name,
               Price = x.Price,
               Quantity = x.Quantity,
               SupplierId = x.SupplierId
            })
            .ToList();

            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Count()}";
        }

        // Import Cars
        public static string ImportCars(CarDealerContext context, string inputXml)
        {

            // first variant with foreach
            /*
             const string root = "Cars";


            var carsDto = XmlConverter.
                Deserializer<CarInputModel>(inputXml, root);

            var cars = new List<Car>();
            var allParts = context.Parts.Select(x => x.Id).ToList();

            foreach (var currentCar in carsDto)
            {
                var distinctedParts = currentCar.CarPartsInputModel.Select(x => x.Id).Distinct();
                var parts = distinctedParts.Intersect(allParts);

                var car = new Car 
                {
                    Make = currentCar.Make,
                    Model = currentCar.Model,
                    TravelledDistance = currentCar.TraveledDistance,
                };

                foreach (var part in parts)
                {
                    var partCar = new PartCar
                    {
                        PartId = part
                    };

                    car.PartCars.Add(partCar);
                }
                cars.Add(car);
            }
            context.Cars.AddRange(cars);
            context.SaveChanges();

            return $"Successfully imported {cars.Count()}";
             */


            // secondVariant with LINQ

            const string root = "Cars";


            var carsDto = XmlConverter.
                Deserializer<CarInputModel>(inputXml, root);

            var allParts = context.Parts.Select(x => x.Id).ToList();

            var cars = carsDto
                .Select(x => new Car
                {
                    Make = x.Make,
                    Model = x.Model,
                    TravelledDistance = x.TraveledDistance,
                    PartCars = x.CarPartsInputModel.Select(x => x.Id).Distinct()
                    .Intersect(allParts)
                    .Select(pc => new PartCar
                    {
                        PartId = pc
                    })
                    .ToList()
                })
                .ToList();


            context.Cars.AddRange(cars);
            context.SaveChanges();

            return $"Successfully imported {cars.Count()}";

        }

        // Import Customers
        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            const string root = "Customers";
            var customersDto = XmlConverter.Deserializer<CustomerInputModel>(inputXml, root);

            var customers = customersDto.Select(x => new Customer
            {
                Name = x.Name,
                BirthDate = x.BirthDate,
                IsYoungDriver = x.IsYoungDriver
            })
             .ToList();

            context.Customers.AddRange(customers);
            context.SaveChanges();
            return $"Successfully imported {customers.Count()}";  
        }

        // Import Sales

        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            const string root = "Sales";

            var salesDto = XmlConverter.
                 Deserializer<SalesInputModel>(inputXml, root);

            var carsIds = context.Cars.Select(x => x.Id).ToList();

            var sales = salesDto.Where(x => carsIds.Contains(x.CarId)).Select(x => new Sale
            {
                CarId = x.CarId,
                CustomerId = x.CustomerId,
                Discount = x.Discount
            })
            .ToList();

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Count()}";
        }
    }
}
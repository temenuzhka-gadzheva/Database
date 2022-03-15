using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using CarDealer.Data;
using CarDealer.DTO;
using CarDealer.Models;
using Newtonsoft.Json;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var carDealerContext = new CarDealerContext();
            carDealerContext.Database.EnsureDeleted();
            carDealerContext.Database.EnsureCreated();

            //string inputJson = File.ReadAllText("../../../Datasets/suppliers.json");
            //string inputJson = File.ReadAllText("../../../Datasets/parts.json");
              string inputJson = File.ReadAllText("../../../Datasets/cars.json");
             // string inputJson = File.ReadAllText("../../../Datasets/customers.json");
            // var result = ImportSuppliers(carDealerContext, inputJson);
            // var result = ImportParts(carDealerContext, inputJson);
             var result = ImportCars(carDealerContext, inputJson);
            // var result = ImportCustomers(carDealerContext, inputJson);
            Console.WriteLine(result);
        }
       

        //use suppliers.json
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var dtoSuppliers = JsonConvert.DeserializeObject<IEnumerable<SupplierInputModel>>(inputJson);
            var suppliers = dtoSuppliers
                .Select(x => new Supplier 
                {
                    Name = x.Name,
                    IsImporter = x.IsImporter
                })
                .ToList();

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Count()}.";
        }

        //use parts.json
        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var suppliersIds = context.Suppliers
                .Select(x => x.Id)
                .ToList();
            
            var parts = JsonConvert.DeserializeObject<IEnumerable<Part>>(inputJson)
                .Where(s => suppliersIds.Contains(s.SupplierId))
                .ToList();
    

            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Count()}.";
        }

        // use cars.json
        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var dtoCars = JsonConvert
                .DeserializeObject<IEnumerable<CarInputModel>>(inputJson)
                .ToList();
            var cars = new List<Car>();

            foreach (var car in dtoCars)
            {
                var currentCar = new Car()
                {
                    Make = car.Make,
                    Model = car.Model,
                    TravelledDistance = car.TravelledDistance
                };

                foreach (var partId in car?.PartsId.Distinct())
                {
                    currentCar.PartCars.Add(new PartCar
                    {
                        PartId = partId
                    });
                }
                cars.Add(currentCar);
            }

           
            context.Cars.AddRange(cars);
            context.SaveChanges();

            return $"Successfully imported {cars.Count()}.";
        }

        // use customers.json
        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var customers = JsonConvert.DeserializeObject<IEnumerable<Customer>>(inputJson)
                .ToList();

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Count}.";
        }


        // use sales.json
    }
}
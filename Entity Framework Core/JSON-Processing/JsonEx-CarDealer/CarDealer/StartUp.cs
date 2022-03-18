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
            // carDealerContext.Database.EnsureDeleted();
            // carDealerContext.Database.EnsureCreated();

            // string inputJson = File.ReadAllText("../../../Datasets/suppliers.json");
            // string inputJson = File.ReadAllText("../../../Datasets/parts.json");
            // string inputJson = File.ReadAllText("../../../Datasets/cars.json");
            //  string inputJson = File.ReadAllText("../../../Datasets/customers.json");
            // string inputJson = File.ReadAllText("../../../Datasets/sales.json");

            // var result = ImportSuppliers(carDealerContext, inputJson);
            // var result = ImportParts(carDealerContext, inputJson);
            // var result = ImportCars(carDealerContext, inputJson);
            // var result = ImportCustomers(carDealerContext, inputJson);
            // var result = ImportSales(carDealerContext, inputJson);
            // var result = GetOrderedCustomers(carDealerContext);
            //var result = GetCarsFromMakeToyota(carDealerContext);
            //var result = GetLocalSuppliers(carDealerContext);
            // var result = GetCarsWithTheirListOfParts(carDealerContext);
            // var result = GetTotalSalesByCustomer(carDealerContext);
            var result = GetSalesWithAppliedDiscount(carDealerContext);

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
        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var sales = JsonConvert.DeserializeObject<IEnumerable<Sale>>(inputJson)
                .ToList();

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Count}.";
        }

        // export ordered customers
        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var customers = context.Customers
                 .OrderBy(x => x.BirthDate)
                .ThenBy(x => x.IsYoungDriver)
                .Select(x => new
                {
                    Name = x.Name,
                    BirthDate = x.BirthDate.ToString("dd/MM/yyyy"),
                    IsYoungDriver = x.IsYoungDriver
                })

                .ToList();

            var result = JsonConvert.SerializeObject(customers, Formatting.Indented);

            return result;
        }

        // export card with make toyota
        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(x => x.Make.ToLower() == "toyota")
                .OrderBy(x => x.Model)
                .ThenByDescending(x => x.TravelledDistance)
                .Select(x => new
                {
                    Id = x.Id,
                    Make = x.Make,
                    Model = x.Model,
                    TravelledDistance = x.TravelledDistance
                })
                .ToList();

            var result = JsonConvert.SerializeObject(cars, Formatting.Indented);

            return result;
        }

        //Export Local Suppliers
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(x => x.IsImporter == false)
                .Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name,
                    PartsCount = x.Parts.Count()
                })
                .ToList();

            var result = JsonConvert.SerializeObject(suppliers, Formatting.Indented);

            return result;
        }

        //Export Cars With Their List Of Parts
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars
                .Select(x => new
                {
                    car = new
                    {
                        Make = x.Make,
                        Model = x.Model,
                        TravelledDistance = x.TravelledDistance
                    },
                    parts = x.PartCars.Select(p => new
                    {
                        Name = p.Part.Name,
                        Price = p.Part.Price.ToString("F2")
                    })
                }).ToList();

            var result = JsonConvert.SerializeObject(cars, Formatting.Indented);
            return result;
        }

        //Export Total Sales By Customer

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                .Where(x => x.Sales.Any(b => b.CustomerId == x.Id))
                .Select(c => new
                {
                    fullName = c.Name,
                    boughtCars = c.Sales.Count(),
                    spentMoney = c.Sales.Sum(m => m.Car.PartCars.Sum(p => p.Part.Price))
                })
                .OrderByDescending(x => x.spentMoney)
                .ThenByDescending(x => x.boughtCars)
                .ToList();

            var result = JsonConvert.SerializeObject(customers, Formatting.Indented);

            return result;
        }

        // Export Sales With Applied Discount

        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context.Sales
                .Take(10)
                .Select(x => new
                {
                    car = new
                    {
                        Make = x.Car.Make,
                        Model = x.Car.Model,
                        TravelledDistance = x.Car.TravelledDistance
                    },
                    customerName = x.Customer.Name,
                    Discount = x.Discount.ToString("F2"),
                    price = x.Car.Sales.Sum(s => s.Car.PartCars.Sum(p => p.Part.Price)).ToString("F2"),
                    priceWithDiscount = (x.Car.Sales.Sum(s => s.Car.PartCars.Sum(p => p.Part.Price)) -
                                         x.Car.Sales.Sum(s => s.Car.PartCars.Sum(p => p.Part.Price)) *
                                        (x.Discount / 100)).ToString("F2")
                });
            var result = JsonConvert.SerializeObject(sales, Formatting.Indented);
            return result;
        }
    }
}
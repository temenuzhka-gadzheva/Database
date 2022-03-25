using ProductShop.Data;
using ProductShop.DataTransferObjects.Input;
using ProductShop.DataTransferObjects.Output;
using ProductShop.Models;
using ProductShop.XmlHelper;
using System;
using System.IO;
using System.Linq;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new ProductShopContext();

            // context.Database.EnsureDeleted();
            // context.Database.EnsureCreated();

            // right button ->  properties -> copy always
              var usersXml = File.ReadAllText("./Datasets/users.xml");
              var productsXml = File.ReadAllText("./Datasets/products.xml");
              var categoriesXml = File.ReadAllText("./Datasets/categories.xml");
              var categoriesProductsXml = File.ReadAllText("./Datasets/categories-products.xml");
            /* var result =  ImportUsers(context, usersXml);
           /* var result = ImportProducts(context, productsXml);
           /* var result =  ImportCategories(context, categoriesXml);
           /* var result =  ImportCategoryProducts(context, categoriesProductsXml); */

           // var result = GetProductsInRange(context);
           // var result = GetSoldProducts(context);
            var result = GetCategoriesByProductsCount(context);
            Console.WriteLine(result);
        }

        // import users
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            const string root = "Users";
            var usersDto = XmlConverter.
                Deserializer<UserInputModel>(inputXml, root);

            var users = usersDto.
                Select(x => new User
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Age = x.Age
                })
                .ToList();

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Count()}";
        }

        // import products
        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            const string root = "Products";
            var productDto = XmlConverter.
                Deserializer<ProductInputModel>(inputXml, root);

            var products = productDto.
                Select(x => new Product
                {
                    Name = x.Name,
                    Price = x.Price,
                    SellerId = x.SellerId,
                    BuyerId = x.BuyerId
                })
                .ToList();

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Count()}";
        }

        // import categories
        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            const string root = "Categories";
            var categoryDto = XmlConverter.
                Deserializer<CategoryInputModel>(inputXml, root);

            var categories = categoryDto.
                Where(x => x.Name != null).
                Select(x => new Category
                {
                    Name = x.Name,
                })
                .ToList();

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Count()}";
        }

        // import categories and products
        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            const string root = "CategoryProducts";
            var categoryProductDto = XmlConverter.
                Deserializer<CategoryProductInputModel>(inputXml, root);

            var categoriesIds = context.Categories.Select(x => x.Id).ToList();
            var productsIds = context.Products.Select(x => x.Id).ToList();

            var categoryProducts = categoryProductDto.
              Where(x => categoriesIds.Contains(x.CategoryId))
              .Where(x => productsIds.Contains(x.ProductId))
              .Select(x => new CategoryProduct 
              {
                  ProductId = x.ProductId,
                  CategoryId = x.CategoryId
              }) 
              .ToList();

            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Count()}";
        }

        // export products in range

        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products.
                Where(x => x.Price >= 500 && x.Price <= 1000)
                .Select(x => new ProductsOutputModel
                {
                    Name = x.Name,
                    Price = x.Price,
                    Buyer = x.Buyer.FirstName + " " + x.Buyer.LastName
                })
                .OrderBy(x => x.Price)
                .Take(10)
                .ToArray();

            const string root = "Products";

            var result = XmlConverter.Serialize(products, root);
            return result;
        }

        // export sold products
        public static string GetSoldProducts(ProductShopContext context)
        {
            var soldProducts =
                context.Users
                .Where(x => x.ProductsSold.Any())
                .Select(x => new UsersOutputModel
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    SoldProducts = x.ProductsSold
                    .Select(s => new SoldProductOutputModel
                    {
                        Name = s.Name,
                        Price = s.Price
                    })
                    .ToArray()
                })
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .Take(5)
                .ToArray();

            const string root = "Users";

            var result = XmlConverter.Serialize(soldProducts, root);
            return result;
        }

        // export categories By Products Count
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                .Select(x => new CategoryOutputModel
                {
                    Name = x.Name,
                    Count = x.CategoryProducts.Count,
                    AveragePrice = x.CategoryProducts.Average(x => x.Product.Price),
                    TotalRevenue = x.CategoryProducts.Sum(x => x.Product.Price)
                })
                .OrderByDescending(x => x.Count)
                .ThenBy(x => x.TotalRevenue)
                .ToArray();

            const string root = "Categories";

            var result = XmlConverter.Serialize(categories, root);
            return result;
        }

        // export users and products
        public static string GetUsersWithProducts(ProductShopContext context)
        {

        }
    }
}
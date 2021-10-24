
using Demo1._1.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo1._1
{
    class Program
    {
        static void Main(string[] args)
        {
            //to create database in sql manager studio
            var db = new TvContext();
            db.Database.EnsureCreated();

            
        }

        private static void GetNews(TvContext db)
        {
            var news = db.News.Select(x => new
            {
                Name = x.Title,
                CategoryName = x.Category.Title
            });
            foreach (var singleNews in news)
            {
                Console.WriteLine($"{singleNews.CategoryName} => {singleNews.Name}");
            }
        }

        private static void InsertNews(TvContext db)
        {
            db.Categories.Add(new Category
            {
                Title = "The end of the Avendures",
                News = new List<News>
            {
                new News
                {
                    Title = "Avedures save the world from Thanos.",
                    Content = "Avendures collect all stones before" +
                    " Thanos, but lose Black Widow. She sacrifices to" +
                    " save the world and to Avendures win.",
                    Comments = new List<Comment>
                    {
                         new Comment{ Author = "Ben", Content = "wow that`s amazing news"},
                         new Comment{ Author = "Samantha", Content = "avendures are the best team"}
                    }

                },
                new News
                {
                    Title = "Iron Man 2",
                    Content = "Tony Stark present your company.He is the Iron Man and save New York. In his company come Natasha Romanov and help to Tony to save the city from evils."
                }
            }
            });
            db.SaveChanges();
        }
    }
}

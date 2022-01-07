using Demo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Z.EntityFramework.Plus;

namespace Demo
{
    class Projection
    {
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    class Program
    {
        static void Main()
        {
            var db = new MusicHubContext();

            // not collection of songs
            //  db.Database.ExecuteSqlRaw("some request");

            // collection of songs
            // can foreach 
            /*  var songs =  db.Songs.FromSqlRaw("SELECT * FROM Songs WHERE Id <= 10").ToList();

              foreach (var song in songs)
              {
                  Console.WriteLine($"{song.Id} => {song.Name}");
              }*/
            // SQL injection
            /* var maxId = Console.ReadLine();
             var songs =  db.Songs.FromSqlRaw("SELECT * FROM Songs WHERE Id <= " + maxId)
                 .ToList();
             foreach (var song in songs)
             {
                 Console.WriteLine($"{song.Id} => {song.Name}");
             }*/

            // desicion 1 to have not SQL injection
            /*  var maxId = Console.ReadLine();
               var songs =  db.Songs.FromSqlRaw("SELECT * FROM Songs WHERE Id <= {0}", maxId).ToList();
              foreach (var song in songs)
              {
                  Console.WriteLine($"{song.Id} => {song.Name}");
              }*/


            // desicion 2 to have not SQL injection
            // so parameter is correct 
            // db.Songs.FromSqlInterpolated($"SELECT * FROM Songs WHERE Id <= {maxId}").ToList();

            // change tracker
            // this object is attached
            /*  var songs = db.Songs.Where(x => x.Name.Contains("W"));
             
            foreach (var song in songs)
              {
                  Console.WriteLine(song.Name);
                //change 
                  song.CreatedOn = DateTime.UtcNow;
              }
              db.SaveChanges();*/

            // second decision of tracking
            //Object Projection is detached
            /* var songs = db.Songs.Where(x => x.Name.Contains("w"))
                 .Select(x => new Projection
                 {
                     Name = x.Name,
                     CreatedOn = x.CreatedOn
                 });

             foreach (var song in songs)
             {
                 Console.WriteLine(song.Name);
                 //change 
                 song.CreatedOn = DateTime.UtcNow;
             }
             db.SaveChanges()*/

            // how to detached object
            // without AsNoTracking attached object
            /* var songs = db.Songs
                 .AsNoTracking()
                 .Where(x => x.Id <= 10)
                 .ToList();
             foreach (var song in songs)
             {
                Console.WriteLine($"{song.Name}");
                 song.Genre = 1;
             }
             db.SaveChanges();*/


            // when want to detached something one by one
            /*   var songs = db.Songs
                          .Where(x => x.Id <= 10)
                          .ToList();
               foreach (var song in songs)
               {
                   // detached
                   // when use Entry, have approach to methadata of this object 
                   db.Entry(song).State = EntityState.Detached;

                   // attached
                  // db.Entry(song).State = EntityState.Modified;
                   Console.WriteLine(song.Name);
                   song.Writer.Name = "-----------------";
               }
               db.SaveChanges();*/

            // use library ZEntityFramework.Plus.EFCore
            // by delete

            //  db.Songs.Where(x => x.Name.StartsWith("sv")).Delete();

            // by update 

            /* db.Songs.Where(x => x.Id <= 10)
                  .Update(oldSong => new Songs { WriterId = oldSong.Id});*/

            // explicit loading
            /*   var songs = db.Songs.Where(x => x.Id <= 10).ToList();

               foreach (var song in songs)
               {
                   Console.WriteLine(song.Name);

                   db.Entry(song).Collection(x => x.SongsPerformers).Load();
                   Console.WriteLine(song.SongsPerformers.Count());

                   db.Entry(song).Reference(x => x.Writer).Load();
                   Console.WriteLine(song.Writer.Name);
               }*/

            // eager loading
            /* var songs = db.Songs
                 .Include(x => x.Writer)
                 .Include(x => x.SongsPerformers)
                 .Where(x => x.Id <= 10)
                 .ToList();

             foreach (var song in songs)
             {
                 Console.WriteLine(song.Name);
                 Console.WriteLine(song.SongsPerformers.Count());
                 Console.WriteLine(song.Writer.Name);
             }
            */

            // lazy loading
            /* var songs = db.Songs.Where(x => x.Id <= 10).ToList();

             foreach (var song in songs)
             {
                 Console.WriteLine(song.Name);
                 Console.WriteLine(song.SongsPerformers.Count());
                 Console.WriteLine(song.Writer.Name);
             }*/


            // this problem received by lazy loading
            // desicion of N+1 Problem 

            /*  var songs = db.Songs
                  .Where(x => x.Id <= 10)
                  .Select(x => new
                  { 
                      x.Name,
                      SongPerformer = x.SongsPerformers.Count(),
                      Writer = x.Writer.Name 
                  })
                  .ToList();

              foreach (var song in songs)
              {
                  Console.WriteLine(song.Name);
                  Console.WriteLine(song.SongPerformer);
                  Console.WriteLine(song.Writer);
              }*/

            // concurrency problems 

            /*    var db1 = new MusicHubContext();
                var song1 = db1.Songs.FirstOrDefault(x => x.Id == 1);
                var db2 = new MusicHubContext();
                var song2= db1.Songs.FirstOrDefault(x => x.Id == 1);

                song1.Price += 1000;
                song2.Price += 1000;

                db1.SaveChanges();
                db2.SaveChanges();*/

            // song loose money 
            // decision of problem 
            // 1. UPDATE Songs  SET Price  =  Price  + 1000 WHERE Id = 1 
            // 2. add of changed property attribute [ConcurrencyCheck] and verify if some is change this property and save changes 

            // money are correct
            /* var db1 = new MusicHubContext();
             var song1 = db1.Songs.FirstOrDefault(x => x.Id == 1);
             var db2 = new MusicHubContext();
             var song2 = db1.Songs.FirstOrDefault(x => x.Id == 1);

             song1.Price += 1000;


             db1.SaveChanges();

             while (true)
             {
                 try
                 {
                     song2.Price += 1000;
                     db2.SaveChanges();
                     break;
                 }
                 catch
                 {
                     db2 = new MusicHubContext();
                     song2 = db1.Songs.FirstOrDefault(x => x.Id == 1);
                 }
             }*/

            // cascade delete
            var song = db.Songs.Find(29);
            db.Songs.Remove(song);
            db.SaveChanges();

        }

    }
}
 
using Demo.Data;
using System;
using System.Linq;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new MinionsDBContext();

           
        }

        private static void GetHowMinionsQuardEvil(MinionsDBContext db)
        {
            var minionVillain = db.MinionsVillains.GroupBy(x => x.Minion.Name)
                   .Select(x => new { MinionName = x.Key, Count = x.Count() })
                   .ToList();
            foreach (var miniVil in minionVillain)
            {
                Console.WriteLine($"{miniVil.MinionName} guard {miniVil.Count} evil!");
            }
        }

        private static void InsertNewTown(MinionsDBContext db)
        {
            //first create new town
            // second save changes
            // third read towns
            db.Towns.Add(new Town { Name = "Bon" });
            db.SaveChanges();
            var towns = db.Towns.ToList();

            foreach (var town in towns.OrderBy(x => x.Name))
            {
                Console.WriteLine(town.Name);
            }
        }

        private static void GetEveryVillainByName(MinionsDBContext db)
        {
            var villains = db.Villains.ToList();
            foreach (var villain in villains.Where(x => x.Name.StartsWith("N")))
            {
                Console.WriteLine(villain.Name);
            }
        }

        private static void GetMinionWitchFirstLeffterOfNameIsN(MinionsDBContext db)
        {
            var minions = db.Minions.ToList();
            foreach (var minion in minions.Where(x => x.Name.StartsWith("N")))
            {
                Console.WriteLine(minion.Name);
            }
        }

        private static void GetCountOfMinions(MinionsDBContext db)
        {
            Console.WriteLine(db.Minions.Count());
        }
    }
}

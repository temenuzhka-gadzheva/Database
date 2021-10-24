using Demo1._2.Models;
using System;
using System.Linq;

namespace Demo1._2
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new HotelContext();

            var allEmployyes = db.Employees.Count();
            Console.WriteLine(allEmployyes);
        }
    }
}

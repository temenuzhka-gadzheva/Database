using System;
using System.Collections.Generic;
using System.Linq;

namespace ExampleLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            var collection = new List<Student>()
{
   new Student{ Name = "Gogi", Marks = new List<int> { 3,4,5,3,2,4}},
   new Student{ Name = "Pepy", Marks = new List<int> { 3,4,5,6,6,4}},
   new Student{ Name = "Alex", Marks = new List<int> { 6,5,6,5,6,6}},
   new Student{ Name = "Alf", Marks = new List<int> { 6,6,6,6,6,6}}

};

            var groups = collection
                .Where(x => x.Name.StartsWith("A"))
                .Select(x => new
                {
                    Name = x.Name,
                    NameInitial = x.Name.Substring(0, 1),
                    AverageMarks = x.Marks.Average()
                })
                .OrderBy(x => x.AverageMarks)
                .GroupBy(x => x.NameInitial);


            foreach (var student in groups)
            {
                Console.WriteLine(student.Key + " " +
                    string.Join(", ", student.Count()));
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleLinq
{
    public class Student
    {  
        public Student()
        {
            this.Marks = new List<int>();
        }
        public string Name { get; set; }
        public List<int> Marks { get; set; }
    }
}

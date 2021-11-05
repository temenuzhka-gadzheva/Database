using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace attributesDemo
{
    [Serializable]
    [Obsolete]
    [Student("Marty",Name = "Test", CustomProperty =5)]

    class Student
    {
        [Obsolete]
        [Student]
        public void PrintName()
        {

        }
        [Student]
        public int Age { get; set; }
    }
}

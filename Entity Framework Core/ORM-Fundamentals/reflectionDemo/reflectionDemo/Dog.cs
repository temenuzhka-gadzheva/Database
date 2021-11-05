using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reflectionDemo
{
    class Dog
    {
        private string fur = "shaggy";
        private int age = 15;
        internal string fullName = "Roky lovely dog";
        public string nickname = "Rokata";

        public Dog()
        {

        }
        public Dog(string name)
        {
           Name = name;
        }
        public string Name { get; set; }
       
    }
}

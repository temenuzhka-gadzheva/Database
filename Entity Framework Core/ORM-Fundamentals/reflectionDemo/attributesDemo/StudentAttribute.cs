using System;


namespace attributesDemo
{ 
    [AttributeUsage(AttributeTargets.All)]
    // to create my attribute
    class StudentAttribute: Attribute
    {
        public StudentAttribute()
        {

        }
        public StudentAttribute(string name)
        {
            Name = name;
        }
        public string  Name { get; set; }
        public int CustomProperty { get; set; }
    }
  
}

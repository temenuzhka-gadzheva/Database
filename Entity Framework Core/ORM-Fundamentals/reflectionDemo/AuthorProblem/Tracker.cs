using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AuthorProblem
{
    public class Tracker
    {
        public void PrintMethodsByAuthor()
        {
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();

            foreach (var type in types)
            {
                PrintAllMethodAuthors(type);
            
                if (!type.GetCustomAttributes().Any(t => t.GetType() == typeof(AuthorAttribute)))
                {
                    continue;
                }
                AuthorAttribute[] attributes = type.GetCustomAttributes()
                   .Where(t => t.GetType() == typeof(AuthorAttribute))
                   .Select(t => (AuthorAttribute)t)
                   .ToArray();

                foreach (var attr in attributes)
                {
                    Console.WriteLine($"{type.Name} created by {attr.Name}");
                }
            }
        }

        private void PrintAllMethodAuthors(Type type)
        {
            MethodInfo[] methodInfos = type.GetMethods();
            foreach (var method in methodInfos)
            {
                if (!method.GetCustomAttributes().Any(a=>a.GetType() == typeof(AuthorAttribute)))
                {
                    continue; 
                }
                Attribute[] attributes = method.GetCustomAttributes().ToArray();

                foreach (var attr in attributes)
                {
                    if (attr is AuthorAttribute)
                    {
                        AuthorAttribute author = (AuthorAttribute)attr;
                        Console.WriteLine($"{method.Name} is written by {author.Name}");
                    }
                }
            }
        }
}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace reflectionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // reflecting methods
            Type type = typeof(Maths);
            Maths math = new Maths();
            MethodInfo[] methods = type.GetMethods(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Static | BindingFlags.Instance);
            foreach (MethodInfo method in methods)
            {
                ParameterInfo[] methodParamInfos = method.GetParameters();

                var methodParams = methodParamInfos.
                    Select(p => new KeyValuePair<string, string>
                    (p.Name, p.ParameterType.Name)).ToList();

                Console.WriteLine($"{method.Name} => {string.Join(" ", methodParams)}");
                var inputParams = new object[] { 5, 6 };
                if (methodParamInfos.Length > 2)
                {
                    inputParams = new object[] { 5, 6, 7 };
                }
                int result = (int)method.Invoke(math,inputParams);
                Console.WriteLine(result) ;
            }


        }

        private static void GetConcreateConstructor()
        {
            Type type = typeof(Avendure);
            ConstructorInfo concreteConstructor = type.GetConstructor(
                new Type[] { typeof(String) });
            Console.WriteLine(concreteConstructor);
        }

        private static void ReflectingConstructors()
        {
            Type type = typeof(Avendure);
            ConstructorInfo[] constructors = type.GetConstructors();

            foreach (var constructor in constructors)
            {
                ParameterInfo[] paramInfos = constructor.GetParameters();

            }
        }

        private static void MultipleValueEnum()
        {
            Days today = Days.Monday | Days.Thuesday | Days.Wednesday;
            Console.WriteLine(today);
            Console.WriteLine((Days)15);
        }

        private static void GetPrivateFieldsInfo()
        {
            Dog dog = new Dog();
            Type dogType = typeof(Dog);

            FieldInfo[] fields = dogType
                 .GetFields(BindingFlags.NonPublic | BindingFlags.Instance);


            foreach (var field in fields)
            {
                Console.WriteLine($"{field.Name}");
                Console.WriteLine($"{field.FieldType}");
                Console.WriteLine($" Is public {field.IsPublic}");


                var fur = field.GetValue(dog);
                Console.WriteLine($"{fur}");
                Console.WriteLine();

            }
        }

        private static void GetFieldInfo()
        {
            Type dogType = typeof(Dog);
            FieldInfo[] fields = dogType
                 .GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var field in fields)
            {
                Console.WriteLine($"{field.Name}");
                Console.WriteLine($"{field.FieldType}");
                Console.WriteLine($" Is public {field.IsPublic}");
                Console.WriteLine();
            }
        }

        private static void WithValues()
        {
            Type dogType = typeof(Dog);
            Dog dog = (Dog)Activator.CreateInstance(dogType, new object[] { "Roky" });
            Console.WriteLine($"Dog name: {dog.Name}");
        }

        private static void withEmptyConstructor()
        {
            Type dogType = typeof(Dog);
            Dog dog = (Dog)Activator.CreateInstance(dogType);
            Console.WriteLine($"Dog name: {dog.Name}");
        }

        private static void StringBuilderWithArguments()
        {
            Type sbType = typeof(StringBuilder);
            StringBuilder withArguments = (StringBuilder)Activator.
                CreateInstance(sbType, new object[] { "Heyhey" });
            withArguments.Append("Hello");
            Console.WriteLine(withArguments);
        }

        private static void CreateStringbuilderWithReflection()
        {
            //  create new instance with reflection
            Type sbType = typeof(StringBuilder);
            StringBuilder sb = (StringBuilder)Activator.CreateInstance(sbType);
            sb.Append("Hello");
            Console.WriteLine(sb);
        }

        private static void Ex5(Type studentType)
        {
            MethodInfo[] methods = studentType.GetMethods();
            foreach (var method in methods)
            {
                Console.WriteLine(method.Name);
            }
        }

        private static Type Ex4(Type studentType)
        {
            studentType = typeof(Student);
            var interfaces = studentType.GetInterfaces();

            foreach (var type in interfaces)
            {
                Console.WriteLine(type.Name);
            }

            return studentType;
        }

        private static void Ex2()
        {
            // return instance of class Type
            Type stringBuilderType = typeof(StringBuilder);
            Console.WriteLine(stringBuilderType.AssemblyQualifiedName);
            Console.WriteLine(stringBuilderType.Name);
            Console.WriteLine(stringBuilderType.FullName);
            // inheritance object
            Console.WriteLine(stringBuilderType.BaseType);
        }

        private static void Ex1()
        {
            Console.WriteLine("Which class do you want to inspect");
            var className = Console.ReadLine();
            Type stringBuilderType = Type.GetType(className);
            Console.WriteLine(stringBuilderType);
        }
    }
    // to print better
    [Flags]
    enum Days
    {
        Monday = 1,
        Thuesday = 2,
        Wednesday = 4,
        Thursday = 8
    }
}

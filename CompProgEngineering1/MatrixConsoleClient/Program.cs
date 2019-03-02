using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CustomMatrix;

namespace MatrixConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculation();

            Console.WriteLine(new string('-', 50));

            Interfaces();

            Console.WriteLine(new string('-', 50));

            ObjectFields();

            Console.ReadKey();
        }

        public static void Interfaces()
        {
            var typeMatrix = typeof(Matrix);

            Console.WriteLine("Type " + typeMatrix.FullName + " implements interfaces:");
            foreach(var inter in typeMatrix.GetInterfaces())
                Console.WriteLine(" - "  + inter.Name);

            var typeSle = typeof(SLE);

            Console.WriteLine("Type " + typeSle.FullName + " implements interfaces:");
            foreach (var inter in typeSle.GetInterfaces())
                Console.WriteLine(" - " + inter.Name);
        }

        public static void ObjectFields()
        {
            var typeMatrix = typeof(Matrix);

            Console.WriteLine("Type " + typeMatrix.FullName + " fields:");
            foreach (var field in typeMatrix.GetMembers())
            {
                Console.WriteLine(" + Field - " + field.MemberType + " " + field.Name);

            }
            Console.WriteLine("Annotations");
            foreach(var prop in typeMatrix.GetMembers()
                .Select(x => x.GetCustomAttribute<DisplayAttribute>())
                .Where(x => x != null))
            {
                Console.WriteLine("   Name        => " + prop.Name);
                Console.WriteLine("   Description => " + prop.Description);
            }

            var typeSle = typeof(SLE);

            Console.WriteLine("Type " + typeMatrix.FullName + " fields:");
            foreach (var field in typeSle.GetMembers())
            {
                Console.WriteLine(" + Field - " + field.MemberType + " " + field.Name);

            }
            Console.WriteLine("Annotations");
            foreach (var prop in typeSle.GetMembers()
                .Select(x => x.GetCustomAttribute<DisplayAttribute>())
                .Where(x => x != null))
            {
                Console.WriteLine("   Name        => " + prop.Name);
                Console.WriteLine("   Description => " + prop.Description);
            }
        }

        public static void Calculation()
        {
            var m = Matrix.Random(4, 4, DateTime.Now.Millisecond);
            Console.WriteLine("Initial matrix:");
            Console.WriteLine(m.ToString());

            Console.WriteLine("Determinant : " + m.GetDeterminant());
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("Initial SLE");
            var sle = new SLE(4, 4);
            sle.GenerateRandom();
            Console.WriteLine(sle.ToString());

            string answer = string.Empty;
            foreach (var n in sle.CalculateEquation())
                answer += " | " + n.ToString("0.0000");

            Console.WriteLine("SLE koefs: " + answer);
        }
    }
}

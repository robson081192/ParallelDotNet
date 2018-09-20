using System;
using System.Linq;
using System.Threading.Tasks;

namespace Example_2_AsParallel_And_ParallelQuery
{
    class Program
    {
        static void Main(string[] args)
        {
            const int count = 50;
            var items = Enumerable.Range(0, count).ToArray();
            var results = new int[count];

            items.AsParallel().ForAll(x =>
            {
                int newValue = x * x * x;
                Console.Write($"{newValue} ({Task.CurrentId})\t");
                results[x] = newValue;
            });
            //Console.WriteLine();
            //foreach (var result in results)
            //{
            //    Console.WriteLine($"{result}\t");
            //}
            Console.WriteLine();
            Console.WriteLine("--------------------------------");
            Console.WriteLine();

            var cubes = items.AsParallel().AsOrdered().Select(x => x * x * x);
            foreach (var cube in cubes)
            {
                Console.Write($"{cube}\t");
            }
            Console.WriteLine();
            Console.WriteLine("Main program done. Press any key to continue...!");
            Console.ReadKey();
        }
    }
}

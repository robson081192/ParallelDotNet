using System;
using System.Linq;

namespace Example_4_PLINQ_Merge_Options
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = Enumerable.Range(0, 200).ToArray();
            var results = numbers
                .AsParallel()
                .WithMergeOptions(ParallelMergeOptions.NotBuffered)
                .Select(x =>
                {
                    var result = Math.Log10(x);
                    Console.WriteLine($"P {result}\t");
                    return result;
                });

            foreach (var result in results)
            {
                Console.WriteLine($"C {result}\t");
            }

            Console.WriteLine("Main program done. Press any key to continue...!");
            Console.ReadKey();
        }
    }
}

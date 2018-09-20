using System;
using System.Linq;

namespace Example_5_Custom_Aggregation
{
    class Program
    {
        static void Main(string[] args)
        {
            //var sum = Enumerable.Range(1, 1000).Sum();

            //var sum = Enumerable.Range(1, 1000).Aggregate(0, (i, acc) => i + acc);
            var sum = ParallelEnumerable.Range(1, 1000)
                .Aggregate(
                    0,
                    (partialSum, i) => partialSum += i,
                    (total, subTotal) => total += subTotal,
                    i => i
                );
            Console.WriteLine($"Sum = {sum}");



            Console.WriteLine("Main program done. Press any key to continue...!");
            Console.ReadKey();
        }
    }
}

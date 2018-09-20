using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Example_21_Concurrent_Bag
{
    class Program
    {
        static void Main(string[] args)
        {
            var bag = new ConcurrentBag<int>();
            var tasks = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                var i1 = i;
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    bag.Add(i1);
                    Console.WriteLine($"{Task.CurrentId} has added: {i1}.");
                    if (bag.TryPeek(out var result))
                    {
                        Console.WriteLine($"{Task.CurrentId} has peaked the value {result}.");
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());
            if (bag.TryTake(out var last))
            {
                Console.WriteLine($"I got {last}.");
            }
            Console.WriteLine($"Main program done. Press any key to exit...");
            Console.ReadKey();
        }
    }
}

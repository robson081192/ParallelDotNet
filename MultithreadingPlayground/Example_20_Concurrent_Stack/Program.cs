using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Example_20_Concurrent_Stack
{
    class Program
    {
        static void Main(string[] args)
        {
            var stack = new ConcurrentStack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);

            int result;
            if (stack.TryPeek(out result))
            {
                Console.WriteLine($"{result} in on top.");
            }

            if (stack.TryPop(out result))
            {
                Console.WriteLine($"Popped {result}.");
            }

            var items = new int[5];
            int poppedCount;
            if ((poppedCount = stack.TryPopRange(items, 0, 5)) > 0)
            {
                var text = string.Join(", ", items.Select(i => i > 0 ? i.ToString() : string.Empty));
                Console.WriteLine($"Popped following ({poppedCount}) elements: {text}.");
            }
            Console.WriteLine($"Main program done. Press any key to exit...");
            Console.ReadKey();
        }
    }
}

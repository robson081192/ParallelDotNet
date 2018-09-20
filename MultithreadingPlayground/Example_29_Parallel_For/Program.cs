using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Example_29_Parallel_For
{
    class Program
    {
        static IEnumerable<int> Range(int start, int end, int step)
        {
            for (int i = start; i < end; i += step)
            {
                yield return i; 
            }
        }
        static void Main(string[] args)
        {
            #region Parallel.Invoke
            var a = new Action(() => { Console.WriteLine($"First {Task.CurrentId}"); });
            var b = new Action(() =>
            {
                Thread.Sleep(3000);
                Console.WriteLine($"Second {Task.CurrentId}");
            });
            var c = new Action(() => { Console.WriteLine($"Third {Task.CurrentId}"); });

            Parallel.Invoke(a, b, c);
            #endregion
            Console.WriteLine();

            #region Parallel.For
            Parallel.For(1, 11, i =>
            {
                Console.WriteLine($"{i}^2 = {i*i}\t");
            });
            #endregion
            Console.WriteLine();

            #region Parallel.ForEach
            string[] words = {"oh", "what", "a", "mess"};
            Parallel.ForEach(words, word =>
            {
                Console.WriteLine($"{word} length = {word.Length} characters. Task: {Task.CurrentId}");
            });

            Console.WriteLine();
            Parallel.ForEach(Range(0, 10, 2), Console.WriteLine);

            Console.WriteLine();
            var po = new ParallelOptions();
            po.MaxDegreeOfParallelism = 2;
            Parallel.ForEach(Range(0, 10, 2), po, number =>
            {
                Console.WriteLine($"Number = {number}");
                Thread.Sleep(3000);
            });
            #endregion

            Console.WriteLine();
            Console.WriteLine("Main program done. Press any key to continue...!");
            Console.ReadKey();
        }
    }
}

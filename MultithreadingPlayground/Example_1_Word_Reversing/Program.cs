using System;
using System.Linq;
using System.Threading;

namespace Example_1_Word_Reversing
{
    class Program
    {
        static void Main(string[] args)
        {
            string sentence = "the quick brown fox jumped over the lazy dog";

            //  with use of one thread
            //var words = sentence.Split().Select(word => new string(word.Reverse().ToArray()));
            //Console.WriteLine(string.Join(" ", words));

            //  with use of multiple threads
            Thread.Sleep(5000);
            Console.WriteLine("Start working...");
            var words = sentence.Split()
                .AsParallel()
                .AsOrdered()
                .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                .Select(word => new string(word.Reverse().ToArray()));
            Console.WriteLine(string.Join(" ", words));

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}

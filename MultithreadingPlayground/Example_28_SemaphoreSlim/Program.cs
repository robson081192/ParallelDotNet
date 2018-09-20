using System;
using System.Diagnostics.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace Example_28_SemaphoreSlim
{
    class Program
    {
        static void Main(string[] args)
        {
            var semaphore = new SemaphoreSlim(2, 10);
            for (int i = 0; i < 20; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine($"Entering task {Task.CurrentId}.");
                    semaphore.Wait();
                    Console.WriteLine($"Processing task {Task.CurrentId}.");
                });
            }

            while (semaphore.CurrentCount <= 2)
            {
                Console.WriteLine($"Semaphore count: {semaphore.CurrentCount}.");
                Console.ReadKey();
                semaphore.Release(2);
            }

            Console.WriteLine("Main program done. Press any key to continue...!");
            Console.ReadKey();
        }
    }
}

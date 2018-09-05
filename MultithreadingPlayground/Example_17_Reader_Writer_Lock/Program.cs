using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Example_17_Reader_Writer_Lock
{
    class Program
    {
        private static ReaderWriterLockSlim _padlock = new ReaderWriterLockSlim();
        private static Random _random = new Random();
        static void Main(string[] args)
        {
            int x = 0;
            var tasks = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    _padlock.EnterReadLock();
                    Console.WriteLine($"Entered read lock, x = {x}");
                    Thread.Sleep(5000);
                    _padlock.ExitReadLock();
                    Console.WriteLine($"Exited read lock, x = {x}");
                }));
            }

            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch (AggregateException e)
            {
                e.Handle(inner =>
                {
                    Console.WriteLine(inner);
                    return true;
                });
            }

            while (true)
            {
                Console.ReadKey();
                _padlock.EnterWriteLock();
                x = _random.Next(10);
                Console.WriteLine($"Write lock acquired, x = {x}");
                _padlock.ExitWriteLock();
                Console.WriteLine($"Write lock released");
            }

            Console.WriteLine("Main program done. Press any key to exit...");
            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Example_19_Concurrent_Queue
{
    class Program
    {
         const int Threads = 4;

        static void Main(string[] args)
        {
            var queue = InitializeQueue<int>(1000000);
            var tasks = new List<Task>();
            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < Threads; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    while (queue.TryDequeue(out var item))
                    {
                        Console.WriteLine($"Thread: {Thread.CurrentThread.ManagedThreadId} => {item}");
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());
            sw.Stop();
            Console.WriteLine($"Main program done in {sw.ElapsedMilliseconds}ms. Press any key to exit...");
            Console.ReadKey();
        }

        private static ConcurrentQueue<T> InitializeQueue<T>(int count)
        {
            var q = new ConcurrentQueue<T>();
            for (int i = 0; i < count; i++)
            {
                if (typeof(T) == typeof(int))
                {
                    var value = (T)Convert.ChangeType(i, typeof(T));
                    q.Enqueue(value);
                }
                else
                {
                    q.Enqueue(default(T));
                }
            }

            return q;
        }
    }
}

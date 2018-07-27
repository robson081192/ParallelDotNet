using System;
using System.Threading;

namespace Example_5_Locking_1
{
    class Program
    {
        //  Shared private fields
        private static int value1 = 1;
        private static int value2 = 1;

        #region Synchronization object

        private static object syncObj = new object();
        #endregion

        private static void DoWork(object delay)
        {
            Thread.Sleep(int.Parse(delay.ToString()));
            lock (syncObj)
            {
                Console.WriteLine($"Executing thread: {Thread.CurrentThread.Name}");
                if (value2 > 0)
                {
                    Console.WriteLine($"Current thread: {Thread.CurrentThread.Name} => message: {value1 / value2}.");
                    value2 = 0;
                }
            }
        }
        static void Main(string[] args)
        {
            //  Start two threads
            Thread t1 = new Thread(DoWork);
            Thread t2 = new Thread(DoWork);
            t1.Name = "Thread one";
            t2.Name = "Thread two";

            t1.Start(0);
            t2.Start(0);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}

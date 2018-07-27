using System;
using System.Threading;

namespace Example_7_Locking_3_Nested_Locks
{
    class Program
    {
        //  shared private fields
        private static int value1 = 1;
        private static int value2 = 1;

        //  synchronization object
        private static object synchObj = new object();

        //  worker method
        public static void DoWork(object delay)
        {
            Thread.Sleep(int.Parse(delay.ToString()));
            lock (synchObj)
            {
                Console.WriteLine($"Executing thread: {Thread.CurrentThread.Name}");
                if (value2 > 0)
                {
                    DoTheDivision();
                    //value2 = 0;
                }
                Console.WriteLine($"{Thread.CurrentThread.Name} => Release the critical section.");
            }
        }

        //  helper method to do the division
        public static void DoTheDivision()
        {
            lock (synchObj)
            {
                Console.WriteLine($"### DoTheDivision ### => Current thread: {Thread.CurrentThread.Name} => message: {value1 / value2}.");
            }
        }
        static void Main(string[] args)
        {
            //  Start two threads
            Thread t1 = new Thread(DoWork);
            Thread t2 = new Thread(DoWork);
            t1.Name = "Thread one";
            t2.Name = "Thread two";

            Console.WriteLine("Starting the threads...");
            t1.Start(6000);
            t2.Start(0);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}

using System;
using System.Threading;

namespace Example_4_Race_Conditions
{
    class Program
    {
        public static int i;
        public static void DoWork()
        {
            for (i = 0; i < 5; i++)
            {
                Console.Write("*");
            }
        }

        static void Main(string[] args)
        {
            //  Start a new thread, to display 5 stars.
            Thread t = new Thread(DoWork);
            t.Start();

            //  Display additional 5 stars.
            DoWork();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}

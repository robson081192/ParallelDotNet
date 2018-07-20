using System;
using System.Threading;

namespace Example_3_Foreground_Background_Threads
{
    class Program
    {
        static void Main(string[] args)
        {
            //  start background thread
            Thread backgroundThread = new Thread(() =>
            {
                Console.WriteLine("Thread is starting, press ENTER to continue...");
                Console.ReadKey();
            });
            backgroundThread.IsBackground = false;
            backgroundThread.Start();

            //  main thread ends here
        }
    }
}

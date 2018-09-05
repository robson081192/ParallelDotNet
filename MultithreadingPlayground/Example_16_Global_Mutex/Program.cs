using System;
using System.Threading;
using System.Threading.Tasks;

namespace Example_16_Global_Mutex
{
    class Program
    {
        static void Main(string[] args)
        {
            //  TODO: Write an application with multithreaded file access (and I/O operations).
            const string appName = "GlobalMutexTest";
            Mutex mutex;

            try
            {
                mutex = Mutex.OpenExisting(appName);
                Console.WriteLine($"Sorry, {appName} is already running.");
            }
            catch (WaitHandleCannotBeOpenedException ex)
            {
                Console.WriteLine("We can run the program just fine.");
                mutex = new Mutex(false, appName);
                var haveLock = mutex.WaitOne();
                if (haveLock)
                {
                    mutex.ReleaseMutex();
                    Console.WriteLine("Wait handle released. Closing the application.");
                }
            }
            Console.ReadKey();

        }
    }
}

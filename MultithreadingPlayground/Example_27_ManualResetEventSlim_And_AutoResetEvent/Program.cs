using System;
using System.Threading;
using System.Threading.Tasks;

namespace Example_27_ManualResetEventSlim_And_AutoResetEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            #region ManualResetEventSlim
            Console.WriteLine("############\tManualResetEventSlim\t############");
            var manEvent = new ManualResetEventSlim();

            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boiling water");
                Thread.Sleep(5000);
                manEvent.Set();
            });
            var makeTea = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Waiting for water...");
                manEvent.Wait();
                Console.WriteLine("Here is your tea.");
            });
            makeTea.Wait();
            #endregion

            #region AutoResetEvent
            Console.WriteLine("############\tAutoResetEvent\t############");
            var autoEvent = new AutoResetEvent(false);

            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boiling water");
                Thread.Sleep(5000);
                autoEvent.Set();
            });
            var makeTea2 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Waiting for water...");
                autoEvent.WaitOne();
                Console.WriteLine("Here is your tea.");
                var ok = autoEvent.WaitOne(1000);
                if (ok)
                {
                    Console.WriteLine("Enjoy your tea");
                }
                else
                {
                    Console.WriteLine("No tea for you");
                }
            });
            makeTea2.Wait();
            #endregion

            Console.WriteLine("Main program done. Press any key to continue...!");
            Console.ReadKey();
        }
    }
}

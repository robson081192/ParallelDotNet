using System;
using System.Threading;
using System.Threading.Tasks;

namespace Example_9_Waiting_For_Tasks
{
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var t = new Task(() =>
            {
                Console.WriteLine("I take 5 seconds...");
                for (int i = 0; i < 5; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);
                }
                Console.WriteLine("I'm done.");
            });
            t.Start();

            var t2 = Task.Factory.StartNew(() => { Thread.Sleep(3000); });

            //Console.ReadKey();
            //cts.Cancel();

            Task.WaitAll(new[] {t, t2}, 4000, token);

            Console.WriteLine($"Task t status is: {t.Status}");
            Console.WriteLine($"Task t2 status is: {t2.Status}");
            Thread.Sleep(2000);
            Console.WriteLine($"Task t status is: {t.Status} (after additional 2 seconds waiting...)");


            Console.WriteLine("Main program done. Press any key to exit...");
            Console.ReadKey();
        }
    }
}

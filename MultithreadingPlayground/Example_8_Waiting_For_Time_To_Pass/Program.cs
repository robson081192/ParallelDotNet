using System;
using System.Threading;
using System.Threading.Tasks;

namespace Example_8_Waiting_For_Time_To_Pass
{
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            var t = new Task(() =>
            {
                var delay = 5000;
                //  While thread.Sleep the scheduler schanges the execution (a different thread is being run in meantime) - nice
                Console.WriteLine($"Thread will sleep for: {delay} seconds. System scheduler will switch to a different thread.");
                Thread.Sleep(delay);
                
                //  Disarm a bomb!
                Console.WriteLine($"Press any key to disarm the bomb. You have {delay} seconds...");
                bool cancelled = token.WaitHandle.WaitOne(delay);
                Console.WriteLine(cancelled ? "Bomb disarmed" : "BOOOM!!!");

                Console.WriteLine($"Moving on with execution. SpinWait will cause wasting processor cycles, but it is good for a short wait - no thread switching will appear.");
                SpinWait.SpinUntil(() => token.IsCancellationRequested);
                Console.WriteLine("SpinUntil has been executed");
            }, token);
            t.Start();

            Console.ReadKey();
            cts.Cancel();

            Console.WriteLine("Main program done. Press any key to exit...");
            Console.ReadKey();
        }
    }
}

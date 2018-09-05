using System;
using System.Threading;
using System.Threading.Tasks;

namespace Example_6_Task_Cancel
{
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            //  Register an event handler, that runs when the cancellation has been requested.
            token.Register(() => { Console.WriteLine("Cancelation has been requested."); });

            #region Token.ThrowIfCancellationRequested();
            var t = new Task(() =>
            {
                int i = 0;
                while (true)
                {
                    //  Soft cancel
                    //if (token.IsCancellationRequested)
                    //{
                    //    break;
                    //}

                    //  Hard cancel (with exception - recommended way)
                    token.ThrowIfCancellationRequested();
                    Console.WriteLine(i++);
                }
            }, token);
            t.Start();
            #endregion

            #region Token.WaitHandle.WaitOne()
            //var cts2 = new CancellationTokenSource();
            //var token2 = cts2.Token;
            //Task.Factory.StartNew(() =>
            //{
            //    //  token2.ThrowIfCancellationRequested();
            //    Console.WriteLine("Working...");
            //    token2.WaitHandle.WaitOne();
            //    Console.WriteLine("Cancellation of second task has been requested.");
            //});
            #endregion

            Console.ReadKey();
            cts.Cancel();
            Console.ReadKey();
            Console.WriteLine($"Task status => {t.Status.ToString()}");
            Console.WriteLine("Main program done. Press any key to exit...");
            Console.ReadKey();
        }
    }
}

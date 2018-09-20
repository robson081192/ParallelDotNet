using System;
using System.Threading;
using System.Threading.Tasks;

namespace Example_2_Task_Run
{
    class Program
    {
        static void Main(string[] args)
        {
            var t1 = Task.Factory.StartNew(() => { Console.WriteLine("Task created with Task.Factory.StartNew()"); });

            Task<Task<int>> t2 = Task.Factory.StartNew(() =>
            {
                Task<int> inner = Task<int>.Factory.StartNew(() => 123);
                return inner;
            });

            Task<Task<int>> t3 = Task.Factory.StartNew(async () =>
            {
                await Task.Delay(5000);
                return 123;
            });

            Task<int> t4 = Task.Factory.StartNew(async () =>
            {
                await Task.Delay(5000);
                return 123;
            }).Unwrap();

            var outer = Task.Factory.StartNew(async () =>
            {
                int innerResult = await Task.Run(async delegate
                {
                    await Task.Delay(5000);
                    return 123;
                });
            });

            var outer2 = Task.Factory.StartNew(async () =>
            {
                int inner2Result = await await Task.Factory.StartNew(async () =>
                    {
                        await Task.Delay(1000);
                        return 42;
                    },
                    CancellationToken.None,
                    TaskCreationOptions.DenyChildAttach,
                    TaskScheduler.Default);
            });

            var outer3 = Task.Factory.StartNew(async () =>
            {
                //  Creates a task that will complete when any of the supplied tasks has completed.
                //await Task.WhenAny(downloadFromHttp, downloadFromFtp);

                //  Creates a task that will complete when all of the supplied tasks have completed.
                //ait Task.WhenAll(measureTemperature, measurePressure);
            });


            Console.WriteLine("Main program done. Press any key to continue...!");
            Console.ReadKey();
        }
    }
}

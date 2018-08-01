using System;
using System.Threading;
using System.Threading.Tasks;

namespace Example_1_Asynchronous_Data_Calculation
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "MainThread";
            Console.WriteLine("Creating new task, that returns a string value.");
            //  start new Task
            var task = Task<string>.Factory.StartNew(() =>
            {
                Thread.Sleep(2000);
                return "Robert";
            });

            //  user result
            Console.WriteLine($"Your name is: {task.Result}");

            Console.WriteLine("Press any key to show next example.");
            Console.ReadKey();

            Console.WriteLine("Creating new task, that returns nothing.");
            //  start new Task
            var voidTask = Task.Factory.StartNew(() =>
            {
                Thread.CurrentThread.Name = "VoidTask";
                #region thread information
                Console.WriteLine($"Task thread name: {Thread.CurrentThread.Name}");
                Console.WriteLine($"Is background thread: {Thread.CurrentThread.IsBackground}");
                Console.WriteLine($"Is threadpool thread: {Thread.CurrentThread.IsThreadPoolThread}");
                #endregion

                #region throw exception
                throw new InvalidOperationException("Something went wrong!");
                #endregion
                Thread.Sleep(5000);
                Console.WriteLine("VoidTask => Hello World!");
            }, TaskCreationOptions.LongRunning);
            voidTask.Wait();
            Console.WriteLine($"Main thread: {Thread.CurrentThread.Name}");

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}

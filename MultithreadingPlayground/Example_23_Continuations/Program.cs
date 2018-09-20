using System;
using System.Threading;
using System.Threading.Tasks;

namespace Example_23_Continuations
{
    class Program
    {
        static void Main(string[] args)
        {
            var task = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(5000);
                Console.WriteLine("Boiling water"); 
            });
            var task2 = task.ContinueWith(t => { Console.WriteLine($"Completed task {t.Id}, pour water into cup."); });
            Task.WaitAll(task2);

            var task3 = Task.Factory.StartNew(() => "Task 3");
            var task4 = Task.Factory.StartNew(() => "Task 4");
            var task5 = Task.Factory.ContinueWhenAll(new[] {task3, task4}, tasks =>
            {
                Console.WriteLine("Tasks completed!");
                foreach (var t in tasks)
                {
                    Console.WriteLine($"- {t.Result}");
                }
            });
            task5.Wait();

            Console.WriteLine("Main program done. Press any key to continue...!");
            Console.ReadKey();
        }
    }
}

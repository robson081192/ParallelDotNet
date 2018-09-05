using System;
using System.Threading;
using System.Threading.Tasks;

namespace Example_5_Creating_And_Starting_Tasks
{
    class Program
    {
        public static void Write(object c)
        {
            int i = 1000;
            while (i-- > 0)
            {
                Console.Write(c);
            }
        }

        public static int TextLength(object o)
        {
            Console.WriteLine($"\nTask with id {Task.CurrentId} processing object {o}...");
            return o.ToString().Length;
        }
        static void Main(string[] args)
        {
            //  Create new task with task factory.
            Task.Factory.StartNew(() =>
            {
                Write('.');
            });

            //   Create new task with Task constructor.
            var t = new Task(() => Write('?'));
            t.Start();

            //  Write characters from main thread.
            Write('-');

            //  Passing argument in a different way + task constructor.
            var ta = new Task(Write, "hello");
            ta.Start();

            //  Passing argument in a different way + task factory.
            Task.Factory.StartNew(Write, "123");

            //  Returning values from tasks.
            Thread.Sleep(5000);
            string text1 = "testing", text2 = "this";
            var task1 = new Task<int>(TextLength, text1);
            task1.Start();

            Task<int> task2 = Task.Factory.StartNew<int>(TextLength, text2);

            Console.WriteLine($"Length of {text1} is {task1.Result}");
            Console.WriteLine($"Length of {text2} is {task2.Result}");

            Console.WriteLine("Main program done.");
            Console.ReadKey();
        }
    }
}

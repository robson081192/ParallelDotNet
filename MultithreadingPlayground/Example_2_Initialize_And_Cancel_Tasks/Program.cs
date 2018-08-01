using System;
using System.Threading;
using System.Threading.Tasks;

namespace Example_2_Initialize_And_Cancel_Tasks
{
    class Program
    {
        public static void DoTaskWork(object data)
        {
            var taskDto = data as TaskDto;
            Thread.CurrentThread.Name = taskDto.ThreadName; 
            Console.WriteLine($"{Thread.CurrentThread.Name} message: {taskDto.Message}");
            Thread.Sleep(taskDto.Delay);
            Console.WriteLine($"{Thread.CurrentThread.Name} work finished!\n------------------------");
        }
        static void Main(string[] args)
        {
            #region initialize task with use of a task factory
            TaskDto dto = new TaskDto
            {
                ThreadName = "Task_Factory_Example",
                Delay = 3000,
                Message = "Hello World!"
            };
            var taskFactExample = Task.Factory.StartNew(DoTaskWork, dto);
            #endregion
            taskFactExample.Wait();

            #region initialize task with use of a task factory and lambda expression
            TaskDto dto1 = new TaskDto
            {
                ThreadName = "Task_Factory_Lambda_Example",
                Delay = 5000,
                Message = "Hi Again!"
            };
            var taskFactLambdaExample = Task.Factory.StartNew(state =>
            {
                DoTaskWork(state);
            }, dto1);
            #endregion
            taskFactLambdaExample.Wait();

            #region task cancelation
            var cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            cts.CancelAfter(5000);

            var canceledTask = Task.Factory.StartNew(() =>
            {
                //  variable to set execution time of long runing operation (it it runs longer than 5000 ms the cancelation token should work, and the task should be canceled).
                var delay = 10000;
                Thread.CurrentThread.Name = "Canceled_Task";
                //  do some work
                Console.WriteLine($"{Thread.CurrentThread.Name} => start work.");
                
                token.ThrowIfCancellationRequested();
                Thread.Sleep(delay);
                //  do some other work
                Console.WriteLine($"{Thread.CurrentThread.Name} => work finished.");
            });
            #endregion
            Console.WriteLine("Press X to abort or any other key to continue.");
            var key = Console.ReadKey();
            if (key.KeyChar == 'x' || key.KeyChar == 'X')
            {
                cts.Cancel();
            }

            try
            {
                canceledTask.Wait(token);
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine($"\nTask has been canceled by user. Message: {ex.Message}");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }

    class TaskDto
    {
        public string ThreadName { get; set; }
        public string Message { get; set; }
        public int Delay { get; set; }
    }
}

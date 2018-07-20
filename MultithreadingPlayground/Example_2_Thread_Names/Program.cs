using System;
using System.Threading;

namespace Example_2_Thread_Names
{
    class Program
    {
        private const int REPETITIONS = 1000;

        public static void PrintCharacter(object character)
        {
            for (int i = 0; i < REPETITIONS; i++)
            {
                Console.Write(character);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("To start the example press any key...\n");
            Console.ReadKey();

            //  start 10 new threads
            for (int i = 0; i < 10; i++)
            {
                Thread t = new Thread(PrintCharacter);
                t.Name = $"Custom thread {i}";
                t.Start(i.ToString());
            }

            //  set breakpoint here
            int stop = 1;
            
            Console.WriteLine("\nFinished.");
            Console.ReadKey();
        }
    }
}

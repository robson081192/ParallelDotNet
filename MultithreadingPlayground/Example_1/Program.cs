using System;
using System.Threading;

namespace Example_1
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

            //  print A characters in new thread
            Thread t1 = new Thread(new ParameterizedThreadStart(PrintCharacter));
            t1.Start('A');

            #region start new thread without specifying delegate
            Thread t2 = new Thread(PrintCharacter);
            t2.Start('B');
            #endregion

            #region start new thread with lambda expression
            Thread t3 = new Thread(() =>
            {
                PrintCharacter("C");
            });
            t3.Start();
            #endregion

            //  print B characters in main application thread
            PrintCharacter('D');

            Console.WriteLine("\nFinished main thread.");
            Console.ReadKey();
        }
    }
}

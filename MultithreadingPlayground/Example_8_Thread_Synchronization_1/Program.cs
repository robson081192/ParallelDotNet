using System;
using System.Threading;

namespace Example_8_Thread_Synchronization_1
{
    class Program
    {
        //  shared field for work result
        public static int result = 0;

        //  lock handle for shared result
        private static object lockHandle = new object();

        #region AutoResetEvent configuration
        //  event wait handles
        public static AutoResetEvent readyToRead = new AutoResetEvent(false);
        public static AutoResetEvent setResult = new AutoResetEvent(false);
        #endregion

        public static void DoWork()
        {
            while (true)
            {
                int i = result;

                //  simulate long calculation
                Thread.Sleep(1);

                #region Sync write work
                //  wait until main loop is ready to receive result
                if (result % 10 == 0)
                {
                    readyToRead.WaitOne();
                }
                #endregion
                //  return result
                lock (lockHandle)
                {
                    result = i + 1;
                }
                if (result % 10 == 0)
                {
                    setResult.Set();
                }
            }
        }
        static void Main(string[] args)
        {
            //  start new thread
            Thread t = new Thread(DoWork);
            t.Start();

            //  collect results every 10 miliseconds
            for (int i = 0; i < 100; i++)
            {
                #region Sync read work in main loop
                readyToRead.Set();
                #endregion

                #region Ask if worker thread has result ready
                setResult.WaitOne();
                #endregion
                lock (lockHandle)
                {
                    Console.WriteLine(result);
                }

                //  simulate other work
                Thread.Sleep(10);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}

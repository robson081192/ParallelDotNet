using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example_3_Parent_And_Child_Tasks
{
    class Program
    {
        private static List<Task<string>> _tasks = new List<Task<string>>();
        private static string _syncResult = "";

        private static string ReverseString(string s)
        {
            Console.WriteLine($"Reversing string: {s}");
            Thread.Sleep(1000);

            //  suggested when using "standard" .net framework
            var sb = new StringBuilder();
            for (int i = s.Length - 1; i >= 0; i--)
            {
                sb.Append(s[i]);
            }

            return sb.ToString();

            //  suggested when using .net core 2.1
            //return string.Concat(s.Reverse());
        }

        public static void ProcessSentence(string sentence, bool runInParallel)
        {
            var words = sentence.Split();
            if (runInParallel)
            {
                foreach (var word in words)
                {
                    _tasks.Add(Task<string>.Factory.StartNew(() => ReverseString(word),
                        TaskCreationOptions.AttachedToParent | TaskCreationOptions.LongRunning));
                }
            }
            else
            {
                var sb = new StringBuilder();
                foreach (var word in words)
                {
                    sb.Append(ReverseString(word));
                }
                _syncResult = sb.ToString();
            }
        }

        static void Main(string[] args)
        {
            string sentence = "the quick brown fox jumped over the lazy dog";
            bool runInParallel = true;

            #region parent and child tasks
            //  parent task will not complete, until last child task has completed.
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Task.Factory.StartNew(() => { ProcessSentence(sentence, runInParallel); }).Wait();
            sw.Stop();

            
            if (runInParallel)
            {
                var result = new StringBuilder();
                foreach (var task in _tasks)
                {
                    result.Append(task.Result + " ");
                }
                Console.WriteLine($"Reversed sentence: {result}");

                #region verify, that all tasks completed

                for (Int16 i = 0; i < _tasks.Count; i++)
                {
                    Console.WriteLine($"Task {i} complete: {_tasks[i].IsCompleted}");
                }
                #endregion
            }
            else
            {
                Console.WriteLine($"Reversed sentence: {_syncResult}");
            }
            
            Console.WriteLine($"Total runtime: {sw.ElapsedMilliseconds}ms.");
            #endregion

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}

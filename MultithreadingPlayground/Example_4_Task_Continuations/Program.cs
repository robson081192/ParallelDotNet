using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example_4_Task_Continuations
{
    class Program
    {
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

        private static string[] Map(string sentence)
        {
            return sentence.Split();
        }

        private static string[] Process(string[] words)
        {
            for(int i = 0; i < words.Length; i++)
            {
                int index = i;
                Task<string>.Factory.StartNew(
                    () => words[index] = ReverseString(words[index]),
                    TaskCreationOptions.AttachedToParent | TaskCreationOptions.LongRunning);
            }

            return words;
        }

        private static string Reduce(string[] words)
        {
            var sb = new StringBuilder();
            foreach (var word in words)
            {
                sb.Append(word);
                sb.Append(' ');
            }

            return sb.ToString();
        }

        static void Main(string[] args)
        {
            string sentence = "the quick brown fox jumped over the lazy dog";

            #region parent and child tasks
            //  parent task will not complete, until last child task has completed.
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var task = Task<string[]>.Factory.StartNew(() => Map(sentence))
                .ContinueWith<string[]>(t => Process(t.Result))
                .ContinueWith<string>(t => Reduce(t.Result));

            Console.WriteLine($"Reversed sentence: {task.Result}");
            sw.Stop();
            Console.WriteLine($"Total runtime: {sw.ElapsedMilliseconds}ms.");
            #endregion

            Console.WriteLine($"Result of Piglatin(\"Mark Farragher\") = {PigLatin(null) ?? "null"}");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        // TODO: complete this method - return the sentence as pig latin
        public static string PigLatin(string sentence)
        {
            var task = Task<string[]>.Factory.StartNew(() => {
                    if (sentence == null)
                    {
                        return new string[0];
                    }
                    if (sentence == string.Empty)
                    {
                        return new []{string.Empty};
                    }
                return sentence.Split();
                })
                .ContinueWith<string[]>(t =>
                {
                    var words = t.Result;
                    for (int i = 0; i < words.Length; i++)
                    {
                        var index = i;
                        Task.Factory.StartNew(() =>
                            {
                                if (string.IsNullOrEmpty(words[index]))
                                {
                                    return;
                                }
                                var sb = new StringBuilder(words[index].ToLower());
                                var firstLetter = sb[0];
                                sb.Remove(0, 1);
                                sb.Append(firstLetter);
                                sb.Append("ay");
                                words[index] = sb.ToString();
                            },
                            TaskCreationOptions.AttachedToParent | TaskCreationOptions.LongRunning);
                    }

                    return words;
                })
                .ContinueWith<string>(t =>
                {
                    var words = t.Result;
                    if (words.Length == 0)
                    {
                        return null;
                    }
                    else
                    {
                        return string.Join(' ', words);
                    }
                });
            return task.Result;
        }
    }
}

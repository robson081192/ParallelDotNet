using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Example_18_Concurrent_Dictionary
{
    class Program
    {
        private static ConcurrentDictionary<string, string> _capitals = new ConcurrentDictionary<string, string>();

        private static void AddParis()
        {
            var success = _capitals.TryAdd("France", "Paris");
            var who = Task.CurrentId.HasValue ? ("Task " + Task.CurrentId) : "Main thread";
            Console.WriteLine($"{who} {(success ? "added" : "did not add")} the element.");
        }
        static void Main(string[] args)
        {
            Task.Factory.StartNew(AddParis).Wait();
            AddParis();

            //_capitals["Russia"] = "Leningrad";
            _capitals.AddOrUpdate("Russia", "Moscow", (key, oldValue) =>
            {
                return "Moscow";
            });
            Console.WriteLine($"The capital of Russia is {_capitals["Russia"]}");

            //_capitals["Sweden"] = "Uppsala";
            var capOfSweden = _capitals.GetOrAdd("Sweden", "Stockholm");
            Console.WriteLine($"The capital of Sweden is {capOfSweden}");

            string toRemove = "Russia";
            string removed;
            var didRemove = _capitals.TryRemove(toRemove, out removed);
            Console.WriteLine(
                didRemove ? $"We just removed {removed}." : $"Failed to remove the capital of {toRemove}.");

            foreach (var capital in _capitals)
            {
                Console.WriteLine($" - {capital.Value} is the capital of {capital.Key}.");
            }

            Console.WriteLine("Main program done. Press any key to exit...");
            Console.ReadKey();
        }
    }
}

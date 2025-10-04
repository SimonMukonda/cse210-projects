using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace MindfulnessProgram
{
    public class ListingActivity : Activity
    {
        private readonly Queue<string> _promptQueue;

        private static readonly string[] Prompts = new[]
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        public ListingActivity() : base("Listing Activity",
            "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
        {
            _promptQueue = new Queue<string>(ShuffleArray(Prompts));
        }

        protected override void Run()
        {
            if (_promptQueue.Count == 0)
            {
                foreach (var p in ShuffleArray(Prompts)) _promptQueue.Enqueue(p);
            }
            string prompt = _promptQueue.Dequeue();
            Console.WriteLine(prompt + "\n");
            Console.WriteLine("You will have a short countdown, then begin listing items. Press Enter after each item.\n");
            Console.WriteLine("Get ready...");
            Countdown(5);

            Stopwatch sw = Stopwatch.StartNew();
            List<string> items = new List<string>();
            while (sw.Elapsed.TotalSeconds < DurationSeconds)
            {
                Console.Write("- ");
                string entry = ReadLineWithTimeout((int)Math.Max(1, DurationSeconds - (int)sw.Elapsed.TotalSeconds));
                if (!string.IsNullOrWhiteSpace(entry)) items.Add(entry.Trim());
                if (sw.Elapsed.TotalSeconds >= DurationSeconds) break;
            }

            Console.WriteLine($"\nYou listed {items.Count} items.");
            if (items.Count > 0)
            {
                Console.WriteLine("Items:");
                for (int i = 0; i < items.Count; i++)
                {
                    Console.WriteLine($" {i + 1}. {items[i]}");
                }
            }
            LogCompletion();
        }

        private static string[] ShuffleArray(string[] array)
        {
            Random r = new Random();
            return array.OrderBy(x => r.Next()).ToArray();
        }

        private string ReadLineWithTimeout(int timeoutSeconds)
        {
            StringBuilder sb = new StringBuilder();
            Stopwatch sw = Stopwatch.StartNew();
            ConsoleKeyInfo key;
            while (sw.Elapsed.TotalSeconds < timeoutSeconds)
            {
                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey(intercept: true);
                    if (key.Key == ConsoleKey.Enter)
                    {
                        Console.WriteLine();
                        break;
                    }
                    else if (key.Key == ConsoleKey.Backspace)
                    {
                        if (sb.Length > 0)
                        {
                            sb.Length--;
                            Console.Write("\b \b");
                        }
                    }
                    else
                    {
                        sb.Append(key.KeyChar);
                        Console.Write(key.KeyChar);
                    }
                }
                else
                {
                    Thread.Sleep(50);
                }
            }
            return sb.ToString();
        }
    }
}

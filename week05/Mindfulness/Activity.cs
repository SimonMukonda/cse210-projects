using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace MindfulnessProgram
{
    public abstract class Activity
    {
        private string _activityName;
        private string _description;
        private int _durationSeconds;

        public string ActivityName => _activityName;
        public string Description => _description;
        protected int DurationSeconds => _durationSeconds;

        protected Activity(string name, string description)
        {
            _activityName = name;
            _description = description;
        }

        public void Start()
        {
            Console.Clear();
            Console.WriteLine($"=== {ActivityName} ===\n");
            Console.WriteLine(Description + "\n");
            _durationSeconds = PromptDurationSeconds();
            Console.WriteLine("Get ready...\n");
            PauseWithSpinner(3);
            Run();
            Finish();
        }

        protected void Finish()
        {
            Console.WriteLine();
            Console.WriteLine("Well done!");
            Console.WriteLine($"You have completed the {ActivityName} for {DurationSeconds} seconds.");
            PauseWithSpinner(3);
        }

        private int PromptDurationSeconds()
        {
            while (true)
            {
                Console.Write("Enter duration in seconds: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int seconds) && seconds > 0)
                {
                    return seconds;
                }
                Console.WriteLine("Please enter a positive integer number of seconds.");
            }
        }

        protected void PauseWithSpinner(int seconds)
        {
            const string spinner = "|/-\\";
            Stopwatch sw = Stopwatch.StartNew();
            int idx = 0;
            while (sw.Elapsed.TotalSeconds < seconds)
            {
                Console.Write(spinner[idx % spinner.Length]);
                Thread.Sleep(250);
                Console.Write('\b');
                idx++;
            }
            Console.WriteLine();
        }

        protected void Countdown(int seconds)
        {
            for (int i = seconds; i >= 1; i--)
            {
                Console.Write($"{i} ");
                Thread.Sleep(1000);
            }
            Console.WriteLine();
        }

        protected abstract void Run();

        protected void LogCompletion()
        {
            try
            {
                string logLine = $"{DateTime.Now:u} | {ActivityName} | {DurationSeconds} s";
                File.AppendAllLines("mindfulness_log.txt", new[] { logLine });
            }
            catch { }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MindfulnessProgram
{
    public class ReflectionActivity : Activity
    {
        private readonly Queue<string> _promptQueue;
        private readonly Queue<string> _questionQueue;

        private static readonly string[] Prompts = new[]
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private static readonly string[] Questions = new[]
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        public ReflectionActivity() : base("Reflection Activity",
            "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
        {
            _promptQueue = new Queue<string>(ShuffleArray(Prompts));
            _questionQueue = new Queue<string>(ShuffleArray(Questions));
        }

        protected override void Run()
        {
            if (_promptQueue.Count == 0)
            {
                foreach (var p in ShuffleArray(Prompts)) _promptQueue.Enqueue(p);
            }
            string prompt = _promptQueue.Dequeue();
            Console.WriteLine(prompt + "\n");
            Console.WriteLine("When you have something in mind, press Enter to continue...");
            Console.ReadLine();

            Stopwatch sw = Stopwatch.StartNew();
            while (sw.Elapsed.TotalSeconds < DurationSeconds)
            {
                if (_questionQueue.Count == 0)
                {
                    foreach (var q in ShuffleArray(Questions)) _questionQueue.Enqueue(q);
                }
                string qn = _questionQueue.Dequeue();
                Console.WriteLine(qn);
                int pause = 5;
                int remaining = (int)Math.Max(0, DurationSeconds - sw.Elapsed.TotalSeconds);
                if (remaining < pause)
                {
                    PauseWithSpinner(remaining);
                    break;
                }
                else
                {
                    PauseWithSpinner(pause);
                }
            }
            LogCompletion();
        }

        private static string[] ShuffleArray(string[] array)
        {
            Random r = new Random();
            return array.OrderBy(x => r.Next()).ToArray();
        }
    }
}

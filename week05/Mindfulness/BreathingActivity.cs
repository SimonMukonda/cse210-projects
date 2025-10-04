using System;
using System.Diagnostics;
using System.Threading;

namespace MindfulnessProgram
{
    public class BreathingActivity : Activity
    {
        public BreathingActivity() : base("Breathing Activity",
            "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
        {
        }

        protected override void Run()
        {
            Stopwatch sw = Stopwatch.StartNew();
            bool inhale = true;
            while (sw.Elapsed.TotalSeconds < DurationSeconds)
            {
                if (inhale)
                {
                    Console.WriteLine("Breathe in...");
                    CountdownWithMin(4);
                }
                else
                {
                    Console.WriteLine("Breathe out...");
                    CountdownWithMin(4);
                }
                inhale = !inhale;
            }
            LogCompletion();
        }

        private void CountdownWithMin(int seconds)
        {
            for (int i = seconds; i >= 1; i--)
            {
                Console.Write($"{i} ");
                Thread.Sleep(1000);
            }
            Console.WriteLine();
        }
    }
}

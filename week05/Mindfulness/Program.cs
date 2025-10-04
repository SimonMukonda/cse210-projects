using System;
using System.IO;
using System.Linq;

namespace MindfulnessProgram
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Mindfulness Program";
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Mindfulness Program\n");
                Console.WriteLine("Please choose an activity:");
                Console.WriteLine("1) Breathing Activity");
                Console.WriteLine("2) Reflection Activity");
                Console.WriteLine("3) Listing Activity");
                Console.WriteLine("4) View log file (quick)");
                Console.WriteLine("5) Exit");
                Console.Write("Select an option (1-5): ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        var breathing = new BreathingActivity();
                        breathing.Start();
                        PromptReturnToMenu();
                        break;
                    case "2":
                        var reflection = new ReflectionActivity();
                        reflection.Start();
                        PromptReturnToMenu();
                        break;
                    case "3":
                        var listing = new ListingActivity();
                        listing.Start();
                        PromptReturnToMenu();
                        break;
                    case "4":
                        ShowLogFile();
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Press Enter to try again.");
                        Console.ReadLine();
                        break;
                }
            }
            Console.WriteLine("Goodbye!");
        }

        private static void PromptReturnToMenu()
        {
            Console.WriteLine("\nPress Enter to return to the main menu.");
            Console.ReadLine();
        }

        private static void ShowLogFile()
        {
            Console.Clear();
            Console.WriteLine("Activity Log:\n");
            try
            {
                if (File.Exists("mindfulness_log.txt"))
                {
                    var lines = File.ReadAllLines("mindfulness_log.txt");
                    foreach (var l in lines.Reverse())
                    {
                        Console.WriteLine(l);
                    }
                }
                else
                {
                    Console.WriteLine("No log file found yet. Complete an activity to create one.");
                }
            }
            catch
            {
                Console.WriteLine("Unable to read log file.");
            }
            Console.WriteLine("\nPress Enter to return to the menu.");
            Console.ReadLine();
        }
    }
}

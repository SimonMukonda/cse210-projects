using System;
using System.Collections.Generic;
using System.IO;

// Class representing a single Journal Entry
public class JournalEntry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public DateTime Date { get; set; }

    public JournalEntry(string prompt, string response)
    {
        Prompt = prompt;
        Response = response;
        Date = DateTime.Now;
    }

    public override string ToString()
    {
        return $"{Date.ToShortDateString()} - Prompt: {Prompt}\nResponse: {Response}\n";
    }
}

// Class to manage the Journal
public class Journal
{
    private List<JournalEntry> _entries = new List<JournalEntry>();

    // Add a new entry
    public void AddEntry(string prompt, string response)
    {
        _entries.Add(new JournalEntry(prompt, response));
    }

    // Display all entries
    public void DisplayEntries()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("No journal entries yet.");
            return;
        }

        foreach (var entry in _entries)
        {
            Console.WriteLine(entry.ToString());
        }
    }

    // Save journal to file
    public void SaveToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var entry in _entries)
            {
                writer.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}");
            }
        }
        Console.WriteLine("Journal saved successfully.");
    }

    // Load journal from file
    public void LoadFromFile(string filename)
    {
        if (File.Exists(filename))
        {
            _entries.Clear();
            string[] lines = File.ReadAllLines(filename);
            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length == 3)
                {
                    JournalEntry entry = new JournalEntry(parts[1], parts[2])
                    {
                        Date = DateTime.Parse(parts[0])
                    };
                    _entries.Add(entry);
                }
            }
            Console.WriteLine("Journal loaded successfully.");
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }
}

// Main Program
public class Program
{
    static void Main(string[] args)
    {
        Journal myJournal = new Journal();
        Random rand = new Random();

        // Predefined prompts
        string[] prompts = {
            "What was the best part of my day?",
            "How did I see the hand of God in my life today?",
            "What was the most challenging thing I faced today?",
            "If I could do one thing better tomorrow, what would it be?",
            "What did I learn today?"
        };

        bool running = true;
        while (running)
        {
            Console.WriteLine("\nJournal Program Menu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display journal entries");
            Console.WriteLine("3. Save journal to file");
            Console.WriteLine("4. Load journal from file");
            Console.WriteLine("5. Quit");
            Console.Write("Choose an option (1-5): ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    // Pick random prompt
                    string prompt = prompts[rand.Next(prompts.Length)];
                    Console.WriteLine($"\nPrompt: {prompt}");
                    Console.Write("Your response: ");
                    string response = Console.ReadLine();
                    myJournal.AddEntry(prompt, response);
                    break;

                case "2":
                    myJournal.DisplayEntries();
                    break;

                case "3":
                    Console.Write("Enter filename to save (e.g., journal.txt): ");
                    string saveFile = Console.ReadLine();
                    myJournal.SaveToFile(saveFile);
                    break;

                case "4":
                    Console.Write("Enter filename to load (e.g., journal.txt): ");
                    string loadFile = Console.ReadLine();
                    myJournal.LoadFromFile(loadFile);
                    break;

                case "5":
                    running = false;
                    Console.WriteLine("Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }
}

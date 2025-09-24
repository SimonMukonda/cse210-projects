using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureMemorizer
{
    // =======================
    // Class: Reference
    // Represents a scripture reference like "John 3:16" or "Proverbs 3:5-6"
    // =======================
    public class Reference
    {
        private string _book;
        private int _chapter;
        private int _startVerse;
        private int? _endVerse; // Nullable for optional range

        // Constructor for single verse
        public Reference(string book, int chapter, int verse)
        {
            _book = book;
            _chapter = chapter;
            _startVerse = verse;
            _endVerse = null;
        }

        // Constructor for a verse range
        public Reference(string book, int chapter, int startVerse, int endVerse)
        {
            _book = book;
            _chapter = chapter;
            _startVerse = startVerse;
            _endVerse = endVerse;
        }

        public string GetDisplayText()
        {
            if (_endVerse.HasValue)
                return $"{_book} {_chapter}:{_startVerse}-{_endVerse}";
            else
                return $"{_book} {_chapter}:{_startVerse}";
        }
    }

    // =======================
    // Class: Word
    // Represents a single word in the scripture
    // =======================
    public class Word
    {
        private string _text;
        private bool _isHidden;

        public Word(string text)
        {
            _text = text;
            _isHidden = false;
        }

        public void Hide()
        {
            _isHidden = true;
        }

        public bool IsHidden()
        {
            return _isHidden;
        }

        public string GetDisplayText()
        {
            if (_isHidden)
                return new string('_', _text.Length);
            else
                return _text;
        }
    }

    // =======================
    // Class: Scripture
    // Holds the scripture reference and words
    // =======================
    public class Scripture
    {
        private Reference _reference;
        private List<Word> _words;
        private Random _random = new Random();

        public Scripture(Reference reference, string text)
        {
            _reference = reference;
            _words = text.Split(" ")
                         .Select(word => new Word(word))
                         .ToList();
        }

        public void HideRandomWords(int numberToHide = 3)
        {
            // Get only words not yet hidden (stretch requirement)
            var visibleWords = _words.Where(w => !w.IsHidden()).ToList();

            if (visibleWords.Count == 0) return;

            for (int i = 0; i < numberToHide && visibleWords.Count > 0; i++)
            {
                int index = _random.Next(visibleWords.Count);
                visibleWords[index].Hide();
                visibleWords.RemoveAt(index); // prevent hiding same word again
            }
        }

        public string GetDisplayText()
        {
            string referenceText = _reference.GetDisplayText();
            string scriptureText = string.Join(" ", _words.Select(w => w.GetDisplayText()));
            return $"{referenceText} - {scriptureText}";
        }

        public bool AllWordsHidden()
        {
            return _words.All(w => w.IsHidden());
        }
    }

    // =======================
    // Class: Program
    // Entry point
    // =======================
    class Program
    {
        static void Main(string[] args)
        {
            // Example scripture: Proverbs 3:5-6
            Reference reference = new Reference("Proverbs", 3, 5, 6);
            string text = "Trust in the Lord with all thine heart and lean not unto thine own understanding. " +
                          "In all thy ways acknowledge him, and he shall direct thy paths.";

            Scripture scripture = new Scripture(reference, text);

            while (true)
            {
                Console.Clear();
                Console.WriteLine(scripture.GetDisplayText());
                Console.WriteLine("\nPress ENTER to hide words or type 'quit' to exit.");

                string input = Console.ReadLine();
                if (input?.ToLower() == "quit")
                    break;

                if (scripture.AllWordsHidden())
                {
                    Console.Clear();
                    Console.WriteLine(scripture.GetDisplayText());
                    Console.WriteLine("\nAll words are hidden. Program ending...");
                    break;
                }

                scripture.HideRandomWords(); // Hide a few more words
            }
        }
    }
}


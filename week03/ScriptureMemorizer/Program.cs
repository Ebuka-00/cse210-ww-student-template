using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ScriptureMemorizerProject
{
    /*
     * EXCEEDING REQUIREMENTS - notes for grading:
     *
     * 1. Scripture library with random selection: Scriptures are loaded from
     *    an external, pipe-delimited text file (scriptures.txt) through the
     *    ScriptureLibrary class. If that file is missing, the program falls
     *    back to a small built-in set (LoadDefaults) so it still runs out of
     *    the box. Each time through the outer loop a random scripture is
     *    chosen, and after one is fully memorized the user is asked if
     *    they'd like to practice another random one without restarting.
     *
     * 2. Only unhidden words are chosen when hiding: Scripture.HideRandomWords
     *    filters down to words that are not yet hidden before picking
     *    randomly, so every Enter press guarantees visible progress instead
     *    of possibly re-hiding an already-hidden word (the stretch
     *    challenge from the assignment).
     *
     * 3. Punctuation-aware hiding: Word.GetDisplayText only replaces letters
     *    and digits with underscores, leaving punctuation (commas,
     *    semicolons, periods) visible. This keeps the sentence's rhythm
     *    readable as a memory cue, closer to how people actually practice.
     *
     * 4. Gently increasing difficulty: the number of words hidden per round
     *    starts small (2) and grows slightly as more rounds pass, so the
     *    beginning isn't overwhelming but the ending doesn't drag on one
     *    word at a time.
     */

    /// <summary>
    /// Represents a single word within a scripture. Tracks whether the word
    /// is currently hidden and knows how to render itself either way.
    /// </summary>
    class Word
    {
        private readonly string _text;
        private bool _isHidden;

        public Word(string text)
        {
            _text = text;
            _isHidden = false;
        }

        public bool IsHidden => _isHidden;

        public void Hide()
        {
            _isHidden = true;
        }

        public void Show()
        {
            _isHidden = false;
        }

        /// <summary>
        /// Returns the text to display. If the word is visible, returns the
        /// word as-is. If hidden, returns the word with every letter and
        /// digit replaced by an underscore, leaving punctuation visible.
        /// </summary>
        public string GetDisplayText()
        {
            if (!_isHidden)
            {
                return _text;
            }

            char[] chars = new char[_text.Length];
            for (int i = 0; i < _text.Length; i++)
            {
                chars[i] = char.IsLetterOrDigit(_text[i]) ? '_' : _text[i];
            }
            return new string(chars);
        }
    }

    /// <summary>
    /// Represents a scripture reference, such as "John 3:16" (single verse)
    /// or "Proverbs 3:5-6" (verse range).
    /// </summary>
    class ScriptureReference
    {
        private readonly string _book;
        private readonly int _chapter;
        private readonly int _startVerse;
        private readonly int _endVerse;

        // Constructor for a single-verse reference, e.g. "John 3:16"
        public ScriptureReference(string book, int chapter, int verse)
            : this(book, chapter, verse, verse)
        {
        }

        // Constructor for a verse-range reference, e.g. "Proverbs 3:5-6"
        public ScriptureReference(string book, int chapter, int startVerse, int endVerse)
        {
            _book = book;
            _chapter = chapter;
            _startVerse = startVerse;
            _endVerse = endVerse;
        }

        public string GetDisplayText()
        {
            if (_startVerse == _endVerse)
            {
                return $"{_book} {_chapter}:{_startVerse}";
            }
            return $"{_book} {_chapter}:{_startVerse}-{_endVerse}";
        }

        public override string ToString()
        {
            return GetDisplayText();
        }
    }

    /// <summary>
    /// Represents a full scripture: a reference plus its text, split into
    /// individually hideable Word objects.
    /// </summary>
    class Scripture
    {
        private readonly ScriptureReference _reference;
        private readonly List<Word> _words;
        private static readonly Random _random = new Random();

        public Scripture(ScriptureReference reference, string text)
        {
            _reference = reference;
            _words = text
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(w => new Word(w))
                .ToList();
        }

        /// <summary>
        /// Builds the full display text: the reference, a blank line, then
        /// the scripture text with any hidden words shown as underscores.
        /// </summary>
        public string GetDisplayText()
        {
            string verseText = string.Join(" ", _words.Select(w => w.GetDisplayText()));
            return $"{_reference.GetDisplayText()}\n\n{verseText}";
        }

        /// <summary>
        /// Hides up to numberOfWords words that are not already hidden,
        /// chosen at random.
        /// </summary>
        public void HideRandomWords(int numberOfWords)
        {
            List<Word> hideable = _words.Where(w => !w.IsHidden).ToList();
            int countToHide = Math.Min(numberOfWords, hideable.Count);

            for (int i = 0; i < countToHide; i++)
            {
                int index = _random.Next(hideable.Count);
                hideable[index].Hide();
                hideable.RemoveAt(index);
            }
        }

        public bool AllWordsHidden()
        {
            return _words.All(w => w.IsHidden);
        }
    }

    /// <summary>
    /// Holds a collection of scriptures and can load them from a text file,
    /// falling back to a small built-in set if no file is found. Supports
    /// choosing a random scripture.
    /// </summary>
    class ScriptureLibrary
    {
        private readonly List<Scripture> _scriptures = new List<Scripture>();
        private static readonly Random _random = new Random();

        public int Count => _scriptures.Count;

        /// <summary>
        /// Loads scriptures from a pipe-delimited text file. Expected format
        /// per line: Book|Chapter|StartVerse|EndVerse|Text
        /// (use the same value for StartVerse and EndVerse for a single verse).
        /// Lines beginning with '#' or blank lines are ignored.
        /// </summary>
        public void LoadFromFile(string path)
        {
            if (!File.Exists(path))
            {
                return;
            }

            foreach (string line in File.ReadAllLines(path))
            {
                if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
                {
                    continue;
                }

                string[] parts = line.Split('|');
                if (parts.Length != 5)
                {
                    continue;
                }

                string book = parts[0].Trim();
                if (!int.TryParse(parts[1].Trim(), out int chapter)) continue;
                if (!int.TryParse(parts[2].Trim(), out int startVerse)) continue;
                if (!int.TryParse(parts[3].Trim(), out int endVerse)) continue;
                string text = parts[4].Trim();

                var reference = new ScriptureReference(book, chapter, startVerse, endVerse);
                _scriptures.Add(new Scripture(reference, text));
            }
        }

        /// <summary>
        /// Adds a handful of well-known scriptures so the program still
        /// works even if scriptures.txt is missing.
        /// </summary>
        public void LoadDefaults()
        {
            _scriptures.Add(new Scripture(
                new ScriptureReference("John", 3, 16),
                "For God so loved the world, that he gave his only begotten Son, " +
                "that whosoever believeth in him should not perish, but have everlasting life."));

            _scriptures.Add(new Scripture(
                new ScriptureReference("Proverbs", 3, 5, 6),
                "Trust in the Lord with all thine heart, and lean not unto thine own understanding. " +
                "In all thy ways acknowledge him, and he shall direct thy paths."));

            _scriptures.Add(new Scripture(
                new ScriptureReference("Philippians", 4, 13),
                "I can do all things through Christ which strengtheneth me."));

            _scriptures.Add(new Scripture(
                new ScriptureReference("Joshua", 1, 9),
                "Have not I commanded thee? Be strong and of a good courage; be not afraid, " +
                "neither be thou dismayed: for the Lord thy God is with thee whithersoever thou goest."));
        }

        public Scripture GetRandomScripture()
        {
            if (_scriptures.Count == 0)
            {
                throw new InvalidOperationException("No scriptures are available in the library.");
            }
            int index = _random.Next(_scriptures.Count);
            return _scriptures[index];
        }
    }

    class Program
    {
        private const string ScriptureFilePath = "scriptures.txt";

        static void Main(string[] args)
        {
            ScriptureLibrary library = new ScriptureLibrary();
            library.LoadFromFile(ScriptureFilePath);
            if (library.Count == 0)
            {
                library.LoadDefaults();
            }

            bool keepPracticing = true;
            while (keepPracticing)
            {
                Scripture scripture = library.GetRandomScripture();
                PracticeScripture(scripture);
                keepPracticing = AskToContinue();
            }

            Console.Clear();
            Console.WriteLine("Great work! Keep practicing and the words will stick. Goodbye!");
        }

        /// <summary>
        /// Runs the hide-and-reveal loop for a single scripture until either
        /// the user quits or every word has been hidden.
        /// </summary>
        static void PracticeScripture(Scripture scripture)
        {
            int round = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine(scripture.GetDisplayText());
                Console.WriteLine();

                if (scripture.AllWordsHidden())
                {
                    Console.WriteLine("(All words are now hidden. Well done!)");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    return;
                }

                Console.Write("Press Enter to hide more words, or type 'quit' to exit: ");
                string input = Console.ReadLine();

                if (string.Equals(input?.Trim(), "quit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.Clear();
                    Console.WriteLine("Goodbye!");
                    Environment.Exit(0);
                }

                round++;
                int wordsToHide = 2 + (round / 4); // gently ramps up over time
                scripture.HideRandomWords(wordsToHide);
            }
        }

        static bool AskToContinue()
        {
            Console.Write("\nWould you like to practice another scripture? (y/n): ");
            string response = Console.ReadLine();
            return string.Equals(response?.Trim(), "y", StringComparison.OrdinalIgnoreCase);
        }
    }
}
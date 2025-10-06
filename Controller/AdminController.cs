using System;
using System.Collections.Generic;
using Hangman.Model;

namespace Hangman.Controller
{
    public class AdminController
    {
        private WordProvider wordProvider;

        public AdminController()
        {
            wordProvider = new WordProvider();
        }

        public void Start()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("\nAdmin Menu:");
                Console.WriteLine("1. View all words");
                Console.WriteLine("2. Add word");
                Console.WriteLine("3. Update word");
                Console.WriteLine("4. Delete word");
                Console.WriteLine("5. Exit");
                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewAllWords();
                        break;
                    case "2":
                        AddWord();
                        break;
                    case "3":
                        UpdateWord();
                        break;
                    case "4":
                        DeleteWord();
                        break;
                    case "5":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void ViewAllWords()
        {
            var words = wordProvider.GetAllWords();
            Console.WriteLine("\nWord List:");
            foreach (var word in words)
            {
                Console.WriteLine($"Word: {word.Word}, Hint: {word.Hint}");
            }
        }

        private void AddWord()
        {
            Console.Write("Enter new word: ");
            string word = Console.ReadLine().Trim().ToUpper();
            Console.Write("Enter hint: ");
            string hint = Console.ReadLine().Trim();
            wordProvider.AddWord(new WordEntry(word,hint));
            Console.WriteLine("Word added successfully!");
        }

        private void UpdateWord()
        {
            Console.Write("Enter word to update: ");
            string oldWord = Console.ReadLine().Trim().ToUpper();
            Console.Write("Enter new word: ");
            string newWord = Console.ReadLine().Trim().ToUpper();
            Console.Write("Enter new hint: ");
            string newHint = Console.ReadLine().Trim();
            wordProvider.UpdateWord(oldWord, new WordEntry(newWord,newHint));
            Console.WriteLine("Word updated successfully!");
        }

        private void DeleteWord()
        {
            Console.Write("Enter word to delete: ");
            string word = Console.ReadLine().Trim().ToUpper();
            wordProvider.DeleteWord(word);
            Console.WriteLine("Word deleted successfully!");
        }
    }
}

//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using Hangman.Model;

//namespace Hangman.Model
//{
//    public class WordProvider
//    {
//        private List<WordEntry> words;

//        public WordProvider()
//        {
//            words = new List<WordEntry>();
//            LoadWordsFromCsv();
//        }

//        private void LoadWordsFromCsv()
//        {
//            string filePath = "Model/word.csv"; // path relative to project
//            if (!File.Exists(filePath))
//            {
//                throw new FileNotFoundException($"CSV file not found at {filePath}");
//            }

//            string[] lines = File.ReadAllLines(filePath);

//            foreach (var line in lines)
//            {
//                string[] parts = line.Split(',');
//                if (parts.Length >= 2)
//                {
//                    string word = parts[0].Trim().ToUpper();
//                    string hint = parts[1].Trim();
//                    words.Add(new WordEntry(word, hint));
//                }
//            }
//        }

//        public WordEntry GetRandomWord()
//        {
//            if (words.Count == 0) return null;

//            Random rand = new Random();
//            int index = rand.Next(words.Count);
//            return words[index];
//        }
//    }
//}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hangman.Model;

namespace Hangman.Model
{
    public class WordProvider
    {
        private string filePath = "Model/word.csv";
        private List<WordEntry> words;

        public WordProvider()
        {
            words = LoadWordsFromCSV();
        }

        private List<WordEntry> LoadWordsFromCSV()
        {
            var wordList = new List<WordEntry>();
            if (File.Exists(filePath))
            {
                foreach (var line in File.ReadLines(filePath))
                {
                    var parts = line.Split(',');
                    if (parts.Length == 2)
                    {
                        wordList.Add(new WordEntry (parts[0],parts[1] ));
                    }
                }
            }
            return wordList;
        }

        public List<WordEntry> GetAllWords()
        {
            return words;
        }

        public void AddWord(WordEntry entry)
        {
            words.Add(entry);
            SaveWordsToCSV();
        }

        public void UpdateWord(string oldWord, WordEntry newEntry)
        {
            var existing = words.FirstOrDefault(w => w.Word == oldWord);
            if (existing != null)
            {
                existing.Word = newEntry.Word;
                existing.Hint = newEntry.Hint;
                SaveWordsToCSV();
            }
        }

        public void DeleteWord(string word)
        {
            var existing = words.FirstOrDefault(w => w.Word == word);
            if (existing != null)
            {
                words.Remove(existing);
                SaveWordsToCSV();
            }
        }

        private void SaveWordsToCSV()
        {
            using (var writer = new StreamWriter(filePath))
            {
                foreach (var w in words)
                {
                    writer.WriteLine($"{w.Word},{w.Hint}");
                }
            }
        }

        public WordEntry GetRandomWord()
        {
            Random rand = new Random();
            int index = rand.Next(words.Count);
            return words[index];
        }
    }
}

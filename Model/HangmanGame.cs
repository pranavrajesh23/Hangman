using System;
using System.Collections.Generic;

namespace Hangman.Model
{
    public class HangmanGame
    {

        private string secretWord;
        private string hint;
        private HashSet<char> correctGuesses;
        private HashSet<char> wrongGuesses;
        private int remainingLives;
        private bool hintUsed;

        public HangmanGame(string word, string wordHint, int maxLives = 3)
        {
            secretWord = word;
            hint = wordHint;
            remainingLives = maxLives;
            correctGuesses = new HashSet<char>();
            wrongGuesses = new HashSet<char>();
            hintUsed = false;
        }

        // Process a letter guess
        public bool GuessLetter(char letter)
        {
            letter = char.ToUpper(letter);
            if (secretWord.Contains(letter.ToString()))
            {
                correctGuesses.Add(letter);
                remainingLives = 3;
                return true;
            }
            else
            {
                if (!wrongGuesses.Contains(letter))
                    wrongGuesses.Add(letter);
                remainingLives--;
                return false;
            }
        }

        // Return word with underscores for unguessed letters
        public string GetDisplayWord()
        {
            char[] display = new char[secretWord.Length];
            for (int i = 0; i < secretWord.Length; i++)
            {
                char c = secretWord[i];
                display[i] = correctGuesses.Contains(c) ? c : '_';
            }
            return string.Join(" ", display);
        }

        // Return hint and mark as used
        public string GetHint()
        {
            hintUsed = true;
            return hint;
        }

        public bool IsWordGuessed()
        {
            foreach (char c in secretWord)
            {
                if (!correctGuesses.Contains(c)) return false;
            }
            return true;
        }

        public bool IsGameOver()
        {
            return remainingLives <= 0 || IsWordGuessed();
        }

        // Properties to expose state
        public int RemainingLives => remainingLives;
        public bool HintUsed => hintUsed;
        public HashSet<char> WrongGuesses => wrongGuesses;
        public string Hint => hint; // already in your model

    }
}

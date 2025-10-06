using System;
using System.Collections.Generic;

namespace Hangman.Model
{
    // ? Declare delegate type
    public delegate void GameStateChangedHandler(string displayWord, int remainingLives, HashSet<char> wrongGuesses,GameStatus status);
    public class HangmanGame
    {
        private string secretWord;
        private string hint;
        private HashSet<char> correctGuesses;
        private HashSet<char> wrongGuesses;
        private int remainingLives;
        private GameStatus status;

        // ? Create an event using the delegate
        public event GameStateChangedHandler OnGameStateChanged;

        public HangmanGame(string word, string wordHint, int maxLives = 3)
        {
            secretWord = word.ToUpper();
            hint = wordHint;
            remainingLives = maxLives;
            correctGuesses = new HashSet<char>();
            wrongGuesses = new HashSet<char>();
            status = GameStatus.Playing;
        }

        public string GetSecretWord()
        {
            return secretWord;
        }

        public bool GuessLetter(char letter)
        {
            letter = char.ToUpper(letter);
            bool correct = false;

            if (status != GameStatus.Playing) return false;

            if (secretWord.Contains(letter.ToString()))
            {
                correctGuesses.Add(letter);
                remainingLives = 3;
                correct = true;
            }
            else
            {
                wrongGuesses.Add(letter);
                remainingLives--;
            }

            if (IsWordGuessed())
                status = GameStatus.Won;
            else if (remainingLives <= 0)
                status = GameStatus.Lost;
            else
                status = GameStatus.Playing;

            // ? Notify subscribers (Controller/View)
            OnGameStateChanged?.Invoke(GetDisplayWord(), remainingLives, wrongGuesses,status);

            return correct;
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

        public string GetHint()
        {
            return hint;
        }

        public bool IsGameOver()
        {
            return Status == GameStatus.Won || Status == GameStatus.Lost;
        }

        public bool IsWordGuessed()
        {
            foreach (char c in secretWord)
            {
                if (!correctGuesses.Contains(c)) return false;
            }
            return true;
        }

        // Properties to expose state
        public int RemainingLives => remainingLives;
        public HashSet<char> WrongGuesses => wrongGuesses;
        public string Hint => hint; // already in your model
        public GameStatus Status => status;
    }
}

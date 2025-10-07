using System;
using System.Collections.Generic;
using System.Threading;


namespace Hangman.View
{
    public class HangmanView
    {
        // Display the current game state (word, lives, wrong guesses)
        public void DisplayGameState(string displayWord, int remainingLives, HashSet<char> wrongGuesses,string hint)
        {
            Console.Clear();
            Console.WriteLine("HANGMAN GAME");
            Console.WriteLine("--------------------");
            Console.WriteLine($"Word: {displayWord}");
            Console.WriteLine($"Hint: {hint}");
            Console.WriteLine($"Lives left: {remainingLives}");
            Console.WriteLine($"Wrong guesses: {string.Join(", ", wrongGuesses)}");
            Console.WriteLine("--------------------");
            Console.WriteLine("Enter a letter to guess: ");
        }

        public char GetUserKey()
        {
            char key = '\0';
            while (true)
            {
                var info = Console.ReadKey(true);
                if (char.IsLetter(info.KeyChar))
                {
                    key = char.ToUpper(info.KeyChar);
                    Console.WriteLine(key);
                    Thread.Sleep(200);
                    break;
                }
            }
            return key;
        }
        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
            Thread.Sleep(500);
        }

        public void DisplayWinMessage(string finalWord)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            string suc = @"
          _______
         |/      |
         |       |  
         |
         |       O    
         |      \|/    VAREN DA AYYASAMY
         |       |      
        _|___   / \
        ";
            Console.WriteLine(suc);
            Console.WriteLine("Congratulations! You guessed the word!");
            Console.WriteLine($"The word was: {finalWord}");
            Console.WriteLine("--------------------");
            Console.ResetColor();
        }

        public void DisplayLoseMessage(string finalWord)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            string failure = @"
          _______
         |/      |
         |       |   
         |      (_)   
         |      /|\    IRANDHIDAVA NI PIRANDHAI 
         |       |      
         |      / \
         |
        _|___
        ";
            Console.WriteLine(failure);
            Console.WriteLine($"The word was: {finalWord}");
            Console.WriteLine("--------------------");
            Console.ResetColor();
        }

        public bool AskPlayAgain()
        {
            Console.WriteLine("Do you want to play again? (Y/N): ");
            string input = Console.ReadLine()?.Trim().ToUpper();
            return input == "Y";
        }
    }
}

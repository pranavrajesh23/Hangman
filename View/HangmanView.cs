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

        //Get a single letter guess from the user
        //public char GetUserGuess()
        //{
        //    string input = Console.ReadLine()?.Trim().ToUpper();
        //    if (string.IsNullOrEmpty(input))
        //        return '\0';

        //    return input[0]; // only take first character
        //}

        public char GetUserKey()
        {
            char key = '\0';
            while (true)
            {
                var info = Console.ReadKey(true); // read single key instantly
                if (char.IsLetter(info.KeyChar) || info.KeyChar == '?')
                {
                    key = char.ToUpper(info.KeyChar);
                    Console.WriteLine(key);
                    break;
                }
            }
            return key;
        }

        // Display a message (general purpose)
        public void DisplayMessage(string message)
        {
            //Console.Clear();
            Console.WriteLine(message);
            //Console.WriteLine("Press any key to continue...");
            //Console.ReadKey();
        }

        // Show hint to the user
        public void DisplayHint(string hint)
        {
            Console.WriteLine($"Hint: {hint}");
            //Console.WriteLine("Press any key to continue...");
            //Console.ReadKey();
        }

        // Display winning message
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

        // Display losing message
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

        // Ask if player wants to play again
        public bool AskPlayAgain()
        {
            Console.WriteLine("Do you want to play again? (Y/N): ");
            string input = Console.ReadLine()?.Trim().ToUpper();
            return input == "Y";
        }
    }
}

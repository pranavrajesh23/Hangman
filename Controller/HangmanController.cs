using System;
using Hangman.Model;
using Hangman.View;
using System.Threading;
using System.Collections.Generic;


namespace Hangman.Controller
{
    public class HangmanController
    {
        private HangmanGame game;
        private HangmanView view;
        private WordProvider wordProvider;

        public HangmanController()
        {
            wordProvider = new WordProvider();
            view = new HangmanView();
        }

        public void StartGame()
        {
            bool playAgain = true;

            while (playAgain)
            {
                Console.Clear();
                WordEntry entry = wordProvider.GetRandomWord();

                game = new HangmanGame(entry.Word, entry.Hint);
                game.OnGameStateChanged -= HandleGameStateChanged;
                game.OnGameStateChanged += HandleGameStateChanged;

                view.DisplayGameState(game.GetDisplayWord(), game.RemainingLives, game.WrongGuesses, game.Hint);

                while (!game.IsGameOver())
                {
                    char guess = view.GetUserKey();
                    bool correct = game.GuessLetter(guess);
                    if (correct)
                    {
                        view.DisplayMessage("Correct guess!"); 
                        Thread.Sleep(200);
                        Console.Clear();
                        view.DisplayGameState(game.GetDisplayWord(), game.RemainingLives, game.WrongGuesses, game.Hint);
                    }

                    else
                    {
                        string firstAttempt = @"
         |     
         |         
         |         First Attempt
         |            
         |      
         |
        _|___
        ";

                        string secondAttempt = @"
         ________
         |/      |
         |       |  
         |          
         |            
         |      
         |
        _|___
        ";
                        if (game.RemainingLives == 2)
                        {
                            view.DisplayMessage(firstAttempt); 
                            Thread.Sleep(1500);
                        }
                        else if (game.RemainingLives == 1)
                        {
                            view.DisplayMessage(secondAttempt); 
                            Thread.Sleep(1500);
                        }

                    }
                }
                Console.Clear();
                view.DisplayGameState(game.GetDisplayWord(), game.RemainingLives, game.WrongGuesses, game.Hint);

                if (game.Status == GameStatus.Won)
                {
                    view.DisplayWinMessage(game.GetDisplayWord());
                }
                else if (game.Status == GameStatus.Lost)
                {
                    view.DisplayLoseMessage(game.GetSecretWord());
                }
                playAgain = view.AskPlayAgain();
            }
            view.DisplayMessage("Thanks for playing Hangman!");
        }
        private void HandleGameStateChanged(string displayWord, int remainingLives, HashSet<char> wrongGuesses,GameStatus status)
        {
            if (status == GameStatus.Playing)
            {
                view.DisplayGameState(displayWord, remainingLives, wrongGuesses, game.GetHint());
            }
        }

    }
}

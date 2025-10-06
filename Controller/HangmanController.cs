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
                // 1?? Get random word + hint from the provider
                WordEntry entry = wordProvider.GetRandomWord();

                // 2?? Initialize a new Hangman game
                game = new HangmanGame(entry.Word, entry.Hint);
                game.OnGameStateChanged += HandleGameStateChanged;

                view.DisplayGameState(game.GetDisplayWord(), game.RemainingLives, game.WrongGuesses, game.Hint);

                // 3?? Game loop
                while (!game.IsGameOver())
                {
                    

                    //char guess = view.GetUserGuess();
                    char guess = view.GetUserKey();
                    bool correct = game.GuessLetter(guess);
                    if (correct)
                    {
                        view.DisplayMessage("Correct guess!"); Thread.Sleep(1500);
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
                            view.DisplayMessage(firstAttempt); Thread.Sleep(1500);
                        }
                        else if (game.RemainingLives == 1)
                        {
                            view.DisplayMessage(secondAttempt); Thread.Sleep(1500);
                        }

                    }
                }

                // 4?? Game ended ? check win/loss
                if (game.IsWordGuessed())
                    view.DisplayWinMessage(game.GetDisplayWord());
                else
                    view.DisplayLoseMessage(entry.Word);

                // 5?? Ask if the player wants to play again
                playAgain = view.AskPlayAgain();
            }

            view.DisplayMessage("Thanks for playing Hangman! ??");
        }

        // ? Event handler that runs whenever the game updates
        private void HandleGameStateChanged(string displayWord, int remainingLives, HashSet<char> wrongGuesses)
        {
            view.DisplayGameState(displayWord, remainingLives, wrongGuesses, game.GetHint());
        }

    }
}

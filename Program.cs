using System;
using Hangman.Controller;

namespace Hangman
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //HangmanController controller = new HangmanController();
            //controller.StartGame();
            //Console.WriteLine("\nThanks for playing!");
            //Console.ReadLine();
            Console.WriteLine("Welcome to Hangman!");
            Console.WriteLine("Select your role:");
            Console.WriteLine("1. Admin");
            Console.WriteLine("2. User");
            Console.Write("Enter choice: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                AdminController adminController = new AdminController();
                adminController.Start();
            }
            else if(choice == "2") 
            {
                HangmanController gameController = new HangmanController();
                gameController.StartGame();
            }
        }
    }
}
using System;

namespace Match4
{ 
    internal static class Program
    {
        public static void Main()
        {
            Console.WriteLine("Welcome to Match4!");
            Console.WriteLine("Do you want to view the rules? (y/n)");
            string choice;
            do
            {
                Console.Write("> ");
                choice = (Console.ReadLine() ?? "").ToLower();
            } while (choice != "y" && choice != "n");

            if (choice == "y")
            {
                Game.DisplayRules();
            }
            
            int playerCount;
            Console.WriteLine("How many players do you want to play with? (2-6)");
            do
            {
                Console.Write("> ");
            } while (!(int.TryParse(Console.ReadLine(), out playerCount) && playerCount > 1 && playerCount < 7));
            
            var game = new Game(playerCount);
            game.Start();
        }
    }
}
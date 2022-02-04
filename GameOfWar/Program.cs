using System;
using Dice;

namespace GameOfWar
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var game = new Game();
            var deck = new Deck();
            
            Console.WriteLine("Welcome to Game of War!");
            
            deck.Shuffle();
            
            Console.WriteLine("Dealing cards...");
            
            game.Draw(deck);

            Console.WriteLine("Starting the game...");
            
            while (game.Turn())
            {
                Console.WriteLine($"Player 1 has {game.Player1.Count} cards and Player 2 has {game.Player2.Count} cards");
            }
            
            Console.WriteLine($"The game is over, {(game.Player1.Count > game.Player2.Count ? "Player 1" : "Player 2")} wins!");
        }
    }
}
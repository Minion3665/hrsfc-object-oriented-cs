using System;
using GameUtils;

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

            do
            {
                for (var i = 0; i < game.Players.Length; i++)
                {
                    if (game.Players[i].Empty)
                    {
                        Console.WriteLine($"Player {i + 1} is out");
                        continue;
                    }

                    Console.WriteLine(
                        $"Player {i + 1} has {game.Players[i].Count} cards; their top card is {game.Players[i][0]}.");
                }
            } while (!game.Turn());

            Console.WriteLine($"Everyone but player {game.GetWinner() + 1} ran out of cards! They win the game!!!");
        }
    }
}
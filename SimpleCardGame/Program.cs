using System;
using GameUtils;

namespace SimpleCardGame
{
    internal static class Program
    {
        private static readonly Deck Deck = new Deck();

        private static void Play()
        {
            var player1Score = 0;
            var player2Score = 0;
            while (Deck.Top != null)
            {
                var player1Card = Deck.Draw();
                var player2Card = Deck.Draw();
                Console.WriteLine($"There are {Deck.Count} cards left in the deck.");
                Console.WriteLine($"Player 1's card is the {player1Card} and Player 2's card is the {player2Card}");
                Console.WriteLine($"Player 1 scores {player1Card.Score} and Player 2 scores {player2Card.Score}");
                
                if (player1Card.Score > player2Card.Score)
                {
                    Console.WriteLine("Player 1 wins this round");
                    player1Score++;
                }
                else if (player1Card.Score < player2Card.Score)
                {
                    Console.WriteLine("Player 2 wins this round");
                    player2Score++;
                }
                else
                {
                    Console.WriteLine("It's a tie! No points awarded.");
                }
                
                Console.WriteLine($"Player 1 has won {player1Score} rounds and Player 2 has won {player2Score} rounds so far");;
                Console.WriteLine("Press any key to continue...");
                
                Console.ReadKey();
                Console.Clear();
            }
            
            Console.WriteLine($"It's the end of the game! Player 1 won {player1Score} rounds and Player 2 won {player2Score} rounds");
            
            if (player1Score > player2Score)
            {
                Console.WriteLine("Player 1 wins the game!");
            }
            else if (player1Score < player2Score)
            {
                Console.WriteLine("Player 2 wins the game!");
            }
            else
            {
                Console.WriteLine("It's a tie! Play again to determine the victor!");
            }
            
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

        }

        private static void Main()
        {
            Deck.Shuffle();
            Play();
        }
    }
}

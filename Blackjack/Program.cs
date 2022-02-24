using System;
using GameUtils;

namespace Blackjack
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Blackjack!");
            
            /*var game = new Game();
            var playing = true;
            
            while (playing)
            {
                game.Start();
                
                Console.WriteLine("Thank you for playing, would you like to play again? (y/n)");

                string choice;
                do
                {
                    Console.Write("y/n: ");
                    choice = (Console.ReadLine() ?? "").ToLower();
                } while (choice != "y" && choice != "n");
                
                if (choice == "n")
                {
                    playing = false;
                }
            }*/
            
            var hand = new Hand();
            hand.Add(new Card(Suits.Clubs, Values.Ace));

            Console.WriteLine(hand.Score);
            Console.WriteLine(hand);

            hand.Add(new Card(Suits.Hearts, Values.Ace));

            Console.WriteLine(hand.Score);
            Console.WriteLine(hand);

            
            hand.Add(new Card(Suits.Spades, Values.Ace));

            Console.WriteLine(hand.Score);
            Console.WriteLine(hand);

            
            hand.Add(new Card(Suits.Clubs, Values.Nine));
            
            Console.WriteLine(hand.Score);
            Console.WriteLine(hand);

        }
    }
}
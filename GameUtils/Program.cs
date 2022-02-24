using System;

namespace GameUtils
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var die = new Die();
            Console.WriteLine($"The die rolled a {die.Value}");
            
            die.Roll();
            Console.WriteLine($"The die rolled a {die.Value}");

            var d20 = new Die(20);
            d20.Roll();
            Console.WriteLine($"The d20 rolled a {d20.Value}");
            
            // Now let's roll the die a bunch of times and show the number of times each value was rolled
            var dieCounts = new int[die.SideCount];
            for (var i = 0; i < dieCounts.Length; i++)
            {
                dieCounts[i] = 0;
            }
            
            Console.WriteLine("Rolling the die 100000 times...");
            for (var i = 0; i < 100000; i++)
            {
                die.Roll();
                dieCounts[die.Value - 1]++;
            }
            
            for (var i = 0; i < dieCounts.Length; i++)
            {
                Console.WriteLine($"{i + 1} was rolled {dieCounts[i]} times, which is {(double) dieCounts[i] / 100000 * 100}% of the time");
            }
            
            Console.WriteLine("Now let's play with a deck of cards...");
            var deck = new Deck();
            Console.WriteLine($"The top card is a {deck.Top}");
            Console.WriteLine($"I'm drawing a card... it's a {deck.Draw()}");
            Console.WriteLine($"Now the top card is a {deck.Top}");
            Console.WriteLine($"Let me just shuffle the deck...");
            deck.Shuffle();
            Console.WriteLine($"Now the top card is a {deck.Top}");
        }
    }
}
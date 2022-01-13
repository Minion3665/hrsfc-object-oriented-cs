using System;

namespace Dice
{
    public enum Suits
    {
        Clubs,
        Diamonds,
        Hearts,
        Spades
    }
    
    public class Card
    {
        public readonly Suits Suit;
        public readonly int Value;

        public int Score => Value * 4 + (int) Suit;

        public Card(Suits suit, int value)
        {
            Suit = suit;
            Value = value < 1 || value > 13 ? throw new ArgumentOutOfRangeException(nameof(value), "Your card must have a value between 1 and 13 (inclusive)") : value;
        }

        public static Card FromIndex(int index)
        {
            if (index < 0 || index > 51)
                throw new ArgumentOutOfRangeException(nameof(index), "Your card must have an index between 0 and 51 (inclusive)");
            
            var suit = (Suits) (index / 13);
            var value = index % 13 + 1;
            
            return new Card(suit, value);
        }

        public override string ToString()
        {
            return $"{Value} of {Suit}";
        }
    }
}
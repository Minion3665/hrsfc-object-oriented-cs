using System;
using System.Diagnostics.CodeAnalysis;

namespace Dice
{
    public enum Suits
    {
        Clubs,
        Diamonds,
        Hearts,
        Spades
    }
    
    public enum Values
    {
        Ace = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13
    }
    
    public class Card
    {
        public readonly Suits Suit;
        public readonly Values Value;

        public int Score => (int) Value * 4 + (int) Suit;

        public Card(Suits suit, Values value)
        {
            Suit = suit;
            Value = value;
        }

        public Card (Suits suit, int value)
        {
            Suit = suit;
            Value = value < 1 || value > 13 ? throw new ArgumentOutOfRangeException(nameof(value), "Your card must have a value between 1 and 13 (inclusive)") : (Values) value;
        }
        
        public Card (int suit, int value)
        {
            Suit = suit < 0 || suit > 3 ? throw new ArgumentOutOfRangeException(nameof(suit), "Your card must have a suit between 0 and 3 (inclusive)") : (Suits) suit;
            Value = value < 1 || value > 13 ? throw new ArgumentOutOfRangeException(nameof(value), "Your card must have a value between 1 and 13 (inclusive)") : (Values) value;
        }
        
        public Card (int suit, Values value)
        {
            Suit = suit < 0 || suit > 3 ? throw new ArgumentOutOfRangeException(nameof(suit), "Your card must have a suit between 0 and 3 (inclusive)") : (Suits) suit;
            Value = value;
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
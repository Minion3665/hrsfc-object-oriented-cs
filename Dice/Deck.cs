using System;
using System.Linq;

namespace Dice
{
    public class Deck
    {
        private int _currentCard;
        public int[] Cards { get; private set; }
        private static readonly Random Random = new Random();
        
        public Card Top => _currentCard >= Cards.Length ? null : Card.FromIndex(Cards[_currentCard]);
        
        public Deck()
        {
            Shuffle();
        }
        public void Shuffle()
        {
            // Get an iterable of all the integers from 0 to 51
            var numbers = Enumerable.Range(0, 52);
            
            // Shuffle the deck from the numbers
            Cards =  numbers.OrderBy(x => Random.Next()).ToArray();
            
            // And set the current card to the top
            _currentCard = 0;
        }

        public Card Draw()
        {
            if (_currentCard >= Cards.Length)
            {
                Shuffle();
            }
            
            return Card.FromIndex(Cards[_currentCard++]);
        }
    }
}
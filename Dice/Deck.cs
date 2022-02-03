using System;
using System.Linq;

namespace Dice
{
    public class Deck
    {
        private int _currentCard;
        public CircularQueue<int> Cards { get; private set; }
        public Card Top => Card.FromIndex(Cards.Top);
        
        public int Count => Cards.Count;

        public Deck()
        {
            Sort();
        }
        
        // This method is **DEPRECATED**. Use the Fisher-Yates shuffle instead.
        public void Shuffle()
        {
            // Get an iterable of all the integers from 0 to 51
            var numbers = Enumerable.Range(0, 52);
            
            // Shuffle the deck from the numbers
            Cards =  numbers.OrderBy(x => Random.Next()).ToArray();
            
            // And set the current card to the top
            _currentCard = 0;
        }
        public void FisherYatesShuffle()
        {
            // Shuffle using the Fisher-Yates algorithm
            for (var i = 0; i < Cards.Length - 1; i++)
            {
                var r = Random.Next(i + 1, 52);
                (Cards[i], Cards[r]) = (Cards[r], Cards[i]);
            }
            
            // And set the current card to the top
            _currentCard = 0;
        }

        public void Sort()
        {
            Cards = Enumerable.Range(0, 52).ToArray();
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
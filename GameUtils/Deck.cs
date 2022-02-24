using System;
using System.Linq;

namespace GameUtils
{
    public class Deck
    {
        private int _currentCard;
        public CircularQueue<int> Cards { get; private set; }
        public Card Top => Card.FromIndex(Cards.Top);
        
        public int Count => Cards.Count;

        public Deck()
        {
            Cards = new CircularQueue<int>(Enumerable.Range(0, 52).ToArray());
        }
        
        public void Shuffle()
        {
            Cards.Shuffle();
        }

        public void Sort()
        {
            throw new NotImplementedException();
        }

        public Card Draw()
        {
            return Card.FromIndex(Cards.Pop());
        }
    }
}
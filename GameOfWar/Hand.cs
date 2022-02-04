using System;
using System.Collections.Generic;
using Dice;

namespace GameOfWar
{
    public class Hand
    {
        private readonly List<int> _cards = new List<int>();

        public int Count => _cards.Count;
        public bool Empty => Count == 0;
        
        public Card this[int index]
        {
            get => Card.FromIndex(_cards[index >= 0 ? index : Count + index]);
        }
        
        public void Add(Card card)
        {
            _cards.Add(card.Index);
        }

        public Card Pop()
        {
            if (Empty) throw new InvalidOperationException("Hand is empty");
            var card = _cards[0];
            _cards.RemoveAt(0);
            return Card.FromIndex(card);
        }
    }
}
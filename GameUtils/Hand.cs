using System;
using System.Collections.Generic;

namespace GameUtils
{
    public class Hand
    {
        protected readonly List<int> Cards = new List<int>();

        public int Count => Cards.Count;
        public bool Empty => Count == 0;

        public Card this[int index] => Card.FromIndex(Cards[index >= 0 ? index : Count + index]);

        public void Add(Card card)
        {
            Cards.Add(card.Index);
        }

        public Card Pop()
        {
            if (Empty) throw new InvalidOperationException("Hand is empty");
            var card = Cards[0];
            Cards.RemoveAt(0);
            return Card.FromIndex(card);
        }
    }
}
using System;
using System.Collections.Generic;
using GameUtils;

namespace Blackjack
{
    public class Hand : GameUtils.Hand
    {
        public int Score => GetCards().Item1;
        public override string ToString()
        {
            return string.Join(", ", GetCards().Item2);
        }

        private (int, string[]) GetCards()
        {
            var hardAces = new List<Card>();
            var score = 0;
            var cards = new string[Count];
            for (var i = 0; i < Count; i++)
            {
                var card = this[i];
                if (card.Value == Values.Ace)
                {
                    hardAces.Add(card);
                    score += 11;
                    continue;
                }

                cards[i - hardAces.Count] = card.ToString();
                score += Math.Min((int)card.Value, 10);
            }

            var softAces = new List<Card>();
            while (score > 21 && hardAces.Count > 0)
            {
                score -= 10;
                softAces.Add(hardAces[0]);
                hardAces.RemoveAt(0);
            }

            for (var i = 0; i < softAces.Count; i++)
            {
                cards[i + Count - hardAces.Count - softAces.Count] = softAces[i] + " (Soft)";
            }

            for (var i = 0; i < hardAces.Count; i++)
            {
                cards[i + Count - hardAces.Count] = hardAces[i] + " (Hard)";
            }

            return (score, cards);
        }
    }
}
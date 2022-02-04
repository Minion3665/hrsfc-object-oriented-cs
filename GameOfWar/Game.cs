using System;
using Dice;

namespace GameOfWar
{
    public class Game
    {
        public readonly Hand Player1 = new Hand();
        public readonly Hand Player2 = new Hand();

        public void Draw(Deck deck)
        {
            if (deck.Cards.Count % 2 != 0) deck.Draw();
            while (!deck.Cards.Empty)
            {
                Player1.Add(deck.Draw());
                Player2.Add(deck.Draw());
            }
        }

        public bool Turn()
        {
            var inPlay = new Hand();
            while (!Player1.Empty && !Player2.Empty)
            {
                inPlay.Add(Player1.Pop());
                inPlay.Add(Player2.Pop());
                
                if (inPlay[-1].Value > inPlay[-2].Value)
                {
                    while (inPlay.Count > 0)
                    {
                        Player1.Add(inPlay.Pop());
                    }
                    
                    return true;
                }
                
                if (inPlay[-1].Value < inPlay[-2].Value)
                {
                    while (inPlay.Count > 0)
                    {
                        Player2.Add(inPlay.Pop());
                    }

                    return true;
                }
                
                for (var i = 0; i < 3; i++)
                {
                    if (Player1.Empty || Player2.Empty) break;
                    inPlay.Add(Player1.Pop());
                    inPlay.Add(Player2.Pop());
                } // War is declared, add 3 cards to the table from each player
            }

            return false;
        }
    }
}
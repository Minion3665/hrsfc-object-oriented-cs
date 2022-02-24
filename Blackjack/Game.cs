using System;
using System.Collections.Generic;
using GameUtils;

namespace Blackjack
{
    public class Game
    {
        Deck deck = new Deck();

        public Game()
        {
            
        }
        
        public string AskHitOrStand()
        {
            string input;
            do
            {
                Console.Write("Hit or Stand? (h/s): ");
                input = (Console.ReadLine() ?? "").ToLower();
            } while (input != "h" && input != "s");
            
            return input;
        }

        public void Start()
        {
            
        }
    }
}
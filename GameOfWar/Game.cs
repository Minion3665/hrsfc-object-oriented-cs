using System;
using Dice;

namespace GameOfWar
{
    public class Game
    {
        public readonly Hand[] Players;
        private readonly Hand[] _table;

        public Game()
        {
            var playerCount = AskForPlayerCount();
            Players = new Hand[playerCount];
            _table = new Hand[playerCount];
            
            for (var i = 0; i < playerCount; i++)
            {
                Players[i] = new Hand();
                _table[i] = new Hand();
            }
        }

        private static int AskForPlayerCount()
        {
            int playerCount;
            do {
                Console.Write("How many players would you like? (2-4): ");
            } while (!int.TryParse(Console.ReadLine(), out playerCount) || playerCount < 2 || playerCount > 4);
            
            return playerCount;
        }

        public void Draw(Deck deck)
        {
            if (deck.Cards.Count % 2 != 0) deck.Draw();
            while (deck.Cards.Count >= Players.Length)
            {
                foreach (var player in Players)
                {
                    player.Add(deck.Draw());
                }
            }
        }
        
        public int? GetWinner()
        {
            var winner = null as int?;
            var playersWithCardsLeft = 0;
            for (var i = 0; i < Players.Length; i++)
            {
                if (Players[i].Empty) continue;
                playersWithCardsLeft++;
                winner = i;
            }
            return playersWithCardsLeft == 1 ? winner : null;
        }

        private bool IsGameOver()
        {
            var winner = GetWinner();
            return winner != null;
        }
        
        public bool Turn()
        {
            var turnOver = false;
            while (!turnOver)
            {
                for (var i = 0; i < Players.Length; i++)
                {
                    if (Players[i].Empty) continue;
                    var card = Players[i].Pop();
                    _table[i].Add(card);
                }

                var topPlayerIndex = -1;
                var war = false;
                var maxScore = 0;

                for (var i = 0; i < _table.Length; i++)
                {
                    if (_table[i].Empty) continue;
                    var score = (int)_table[i][-1].Value;
                    if (score < maxScore) continue;
                    if (score == maxScore)
                    {
                        war = true; 
                        continue;
                    }
                    maxScore = score;
                    topPlayerIndex = i;
                    war = false;
                }
                // Following rules from https://www.pagat.com/war/war.html; if there is a tie *all* players take part in the war
                
                if (war)
                {
                    Console.WriteLine("WAR! Players must all play two more cards and then the highest scoring player wins the war.");
                    for (var i = 0; i < Players.Length; i++)
                    {
                        if (Players[i].Empty) continue;
                        var card = Players[i].Pop();
                        _table[i].Add(card);
                        // We only add 1 card here so that the next time around the loop the war can be resolved
                    }
                }
                else
                {
                    foreach (var hand in _table)
                    {
                        while (!hand.Empty)
                        {
                            var card = hand.Pop();
                            Players[topPlayerIndex].Add(card);
                        }
                    }
                    turnOver = true;
                }
            }

            return IsGameOver();
        }
    }
}
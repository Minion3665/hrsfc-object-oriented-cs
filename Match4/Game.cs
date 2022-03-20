using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GameUtils;

namespace Match4
{
    public class Game
    {
        private readonly Deck _deck = new Deck();
        private readonly Match4Player[] _players;

        public Game(int playerCount)
        {
            _players = new Match4Player[playerCount];
            _deck.Shuffle();
        }

        public void Start()
        {
            // Create hands and deal 5 cards to each player
            for (var i = 0; i < _players.Length; i++)
            {
                _players[i] = new Match4Player();
                for (var j = 0; j < 5; j++) _players[i].Add(_deck.Draw());
            }

            var gameOver = false;

            while (!gameOver)
                for (var i = 0; i < _players.Length; i++)
                {
                    gameOver = Turn(i);

                    if (gameOver) break;
                }

            DisplayWinner();
        }

        private bool Turn(int playerNumber)
        {
            var playerTurnOver = false;
            Console.WriteLine(
                $"It's player {playerNumber + 1}'s turn! (Press enter when only player {playerNumber + 1} is looking)");
            Console.ReadLine();
            Console.Clear();
            while (!playerTurnOver)
            {
                var player = _players[playerNumber];
                Console.WriteLine($"It's player {playerNumber + 1}'s turn!");
                Console.WriteLine("Here's your hand:");
                player.PrintHand();
                Console.WriteLine(
                    $"There {(_deck.Count != 1 ? "are" : "is")} {_deck.Count} card{(_deck.Count != 1 ? "s" : "")} left in the deck.");
                Console.WriteLine("What would you like to do?");
                var action = player.AskForAction(_players.Length, playerNumber);
                Values? rank = null;
                if (action != null)
                {
                    var poppedCards = _players[((Action) action).Target].PopCardsWithValue(((Action) action).CardRank);
                    if (poppedCards.Count != 0)
                    {
                        foreach (var poppedCard in poppedCards) player.Add(poppedCard);

                        Console.WriteLine(
                            $"That player had {poppedCards.Count} {((Action) action).CardRank}{(poppedCards.Count != 1 ? "s" : "")}! Your turn continues!");
                        continue;
                    }

                    Console.WriteLine("That player doesn't have any of that rank!");
                    rank = ((Action) action).CardRank;
                }

                Console.WriteLine("Drawing a card");

                var acceptedRanks = new List<Values>();
                if (rank != null) acceptedRanks.Add((Values) rank);
                else
                    acceptedRanks.AddRange(player.GetSetsAndSpares().Item2.Select(spareCard => spareCard.Value));

                Card card = null;
                try
                {
                    card = _deck.Draw();
                }
                catch (Exception e)
                {
                    if (e is QueueEmptyException)
                    {
                        Console.WriteLine("The deck is empty, but you needed to draw a card! The game is over!");
                        return true;
                    }
                }

                Debug.Assert(card != null, nameof(card) + " != null");

                Console.WriteLine($"You drew the {card}!");

                player.Add(card);
                if (acceptedRanks
                    .Contains(card
                        .Value)) // If the player drew a card that doesn't match the rank they asked for, their turn is over
                {
                    Console.WriteLine("You drew a card that matches the rank you needed! Your turn continues...");
                }
                else
                {
                    Console.WriteLine("You drew a card that doesn't match the rank you needed! Your turn is over.");
                    playerTurnOver = true;
                }
            }

            return false;
        }

        public static void DisplayRules()
        {
            Console.WriteLine("== Rules ==");
            Console.WriteLine("    A set is when you have a set of all 4 cards of the same rank.");
            Console.WriteLine("    All players start with 5 cards, the aim is to get as many sets as possible");
            Console.WriteLine(
                "    The game ends when there are no cards left in the deck, and the player with the most sets wins");

            Console.WriteLine("== Actions ==");
            Console.WriteLine(
                "    You can choose to draw a card, or to ask another player for the cards of a certain rank they have.");

            Console.WriteLine("== Asking for cards ==");
            Console.WriteLine("    You need to have a card that is in the rank you ask for.");
            Console.WriteLine("    If the player you ask for doesn't have any of that rank, you draw a card.");
            Console.WriteLine(
                "    If the player you ask for has a card that matches the rank you ask for, you take all of their cards of that rank.");
            Console.WriteLine(
                "    If you get a card that matches the rank you ask for (either by drawing or by asking for another player), your turn continues.");
            Console.WriteLine("    If you get a card that doesn't match the rank you ask for, your turn is over.");

            Console.WriteLine("== Choosing to draw a card ==");
            Console.WriteLine(
                "    You can choose to draw a card, if you get a card that matches one of the ranks in your hand your turn continues, otherwise your turn is over.");

            Console.WriteLine("== Game Over ==");
            Console.WriteLine(
                "    If there are no cards left in the deck and someone needs to draw a card, the game is over.");
        }

        private void DisplayWinner()
        {
            var winningPlayerNumbers = new List<int>();
            var winningPlayerScore = -1;

            for (var i = 0; i < _players.Length; i++)
            {
                var player = _players[i];
                var playerScore = player.Score;
                if (playerScore > winningPlayerScore)
                {
                    winningPlayerNumbers.Clear();
                    winningPlayerScore = playerScore;
                }

                if (playerScore == winningPlayerScore) winningPlayerNumbers.Add(i + 1);
            }

            Console.WriteLine("== Game Over ==");
            Console.WriteLine(winningPlayerNumbers.Count == 1
                ? $"Player {winningPlayerNumbers[0]} won with {winningPlayerScore} sets!"
                : $"Players {string.Join(", ", winningPlayerNumbers)} jointly won with {winningPlayerScore} set{(winningPlayerScore != 1 ? "s" : "")} each!");
        }
    }
}
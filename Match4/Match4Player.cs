using System;
using System.Collections.Generic;
using System.Linq;
using GameUtils;

namespace Match4
{
    public struct Action
    {
        public int Target;
        public Values CardRank;
    }

    public class Match4Player : ScoredHand
    {
        public override int Score => GetSetsAndSpares().Item1.Count;

        public (List<Values>, List<Card>) GetSetsAndSpares()
        {
            var cardsByValue = new Dictionary<Values, List<Card>>();
            foreach (var card in Cards.Select(Card.FromIndex))
            {
                if (!cardsByValue.ContainsKey(card.Value)) cardsByValue.Add(card.Value, new List<Card>());
                cardsByValue[card.Value].Add(card);
            }

            var sets = new List<Values>();
            var spares = new List<Card>();

            foreach (var value in cardsByValue.Keys)
                if (cardsByValue[value].Count == 4)
                    sets.Add(value);
                else
                    spares.AddRange(cardsByValue[value]);

            return (sets, spares);
        }

        public void PrintHand()
        {
            var (sets, spares) = GetSetsAndSpares();
            Console.WriteLine(
                $"You have {sets.Count} set{(sets.Count != 1 ? "s" : "")} and {spares.Count} other cards");
            Console.WriteLine($"Your other cards are: {string.Join(", ", spares.Select(c => c.ToString()))}");
        }

        public Action? AskForAction(int numberOfPlayers, int myPlayerNumber)
        {
            var (_, spares) = GetSetsAndSpares();
            var spareValues = spares.Select(spare => spare.Value).ToHashSet();

            Console.WriteLine("What action do you want to take?");
            Console.WriteLine("(1) Draw a card");
            if (spares.Count > 0) Console.WriteLine("(2) Ask another player for a card");

            int choice;
            do
            {
                Console.Write("> ");
            } while (!(int.TryParse(Console.ReadLine(), out choice) && choice > 0 &&
                       choice <= (spares.Count > 0 ? 2 : 1)));

            if (choice == 1) return null;

            Console.WriteLine("Which player do you want to ask? (1-" + numberOfPlayers + ")");
            int playerNumber;
            do
            {
                Console.Write("> ");
            } while (!(int.TryParse(Console.ReadLine(), out playerNumber) && playerNumber > 0 &&
                       playerNumber <= numberOfPlayers && playerNumber != myPlayerNumber + 1));

            Console.WriteLine($"What value of card do you want to ask for? (One of {string.Join(", ", spareValues)})");
            Values cardValue;
            do
            {
                Console.Write("> ");
            } while (!(Enum.TryParse(Console.ReadLine(), true, out cardValue) && spareValues.Contains(cardValue)));

            return new Action
            {
                CardRank = cardValue,
                Target = playerNumber - 1
            };
        }

        public List<Card> PopCardsWithValue(Values value)
        {
            var cards = Cards.Select(Card.FromIndex).Where(c => c.Value == value).ToList();
            Cards.RemoveAll(c => Card.FromIndex(c).Value == value);
            return cards;
        }
    }
}
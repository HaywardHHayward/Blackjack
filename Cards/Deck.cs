using System;
using System.Collections;
using System.Collections.Generic;

namespace Cards
{
    public class Deck : IEnumerable<Card>
    {

        private readonly Random random = new Random();
        public List<Card> deck = new List<Card>(52);
        public int Count => deck.Count;
        public Card this[int index] { get => deck[index]; set => deck[index] = value; }

        public Deck()
        {
            Suit suit;
            bool ascending;
            Card[] cards = new Card[52];
            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        suit = Suit.Hearts;
                        ascending = true;
                        break;
                    case 1:
                        suit = Suit.Clubs;
                        ascending = true;
                        break;
                    case 2:
                        suit = Suit.Diamonds;
                        ascending = false;
                        break;
                    default:
                        suit = Suit.Spades;
                        ascending = false;
                        break;
                }
                for (int j = 0; j < 13; j++)
                {
                    if (ascending)
                    {
                        cards[(i * 13) + j] = new Card(suit, j + 1);
                    }
                    else
                    {
                        cards[(i * 13) + j] = new Card(suit, 13 - j);
                    }
                }
            }
            deck = new List<Card>(cards);
        }

        public Deck(IEnumerable<Card> cards) => deck = new List<Card>(cards);

        public override string ToString()
        {
            string returnString = "";
            foreach (Card card in deck)
            {
                returnString += card.ToString() + "\n";
            }
            return returnString;
        }

        public Deck Shuffle()
        {
            int n = deck.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                Card value = deck[k];
                deck[k] = deck[n];
                deck[n] = value;
            }
            return this;
        }

        public Deck Clear()
        {
            deck = new List<Card>();
            return this;
        }

        public void Replace(IEnumerable<Card> range) => deck = new List<Card>(range);

        public void Add(Card value) => deck.Add(value);

        public void Insert(int index, Card value) => deck.Insert(index, value);

        public bool Contains(Card value) => deck.Contains(value);

        public bool Remove(Card value) => deck.Remove(value);

        public void RemoveAt(int index) => deck.RemoveAt(index);

        public void AddRange(IEnumerable<Card> range) => deck.AddRange(range);

        public IEnumerator<Card> GetEnumerator() => deck.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => deck.GetEnumerator();

        public void Deal(Deck otherDeck)
        {
            otherDeck.Add(deck[0]);
            deck.RemoveAt(0);
        }

        public void Deal(int removeIndex, Deck otherDeck, int insertIndex = 0)
        {
            otherDeck.Insert(insertIndex, deck[removeIndex]);
            deck.RemoveAt(removeIndex);
        }

        public void DealRange(Deck otherDeck, IEnumerable<Card> cards)
        {
            List<Card> clone = new List<Card>(cards);
            foreach (Card card in clone)
            {
                int lastIndex = deck.LastIndexOf(card);
                otherDeck.Add(card);
                RemoveAt(lastIndex);
            }
        }
    }
}
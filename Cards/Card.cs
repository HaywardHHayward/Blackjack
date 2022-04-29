using System;
namespace Cards
{
    public struct Card
    {
        public readonly Suit suit;
        public readonly Color color;
        public readonly Value value;
        public Card(Suit suit, Value value)
        {
            this.suit = suit;
            this.value = value;
            if (this.suit == Suit.Hearts || this.suit == Suit.Diamonds)
            {
                color = Color.Red;
            }
            else
            {
                color = Color.Black;
            }
        }
        public Card(Suit suit, int number)
        {
            this.suit = suit;
            if (number >= 1 & number <= 13)
            {
                value = (Value)(number - 1);
            }
            else
            {
                throw new IndexOutOfRangeException("Card.number value outside of expected range.");
            }
            if (this.suit == Suit.Hearts || this.suit == Suit.Diamonds)
            {
                color = Color.Red;
            }
            else
            {
                color = Color.Black;
            }
        }

        public override string ToString() => $"{value} of {suit}";
    }
}

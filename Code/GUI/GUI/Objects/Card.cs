using System;

namespace Objects
{
    public class Card
    {
        public enum Suit
        {
            CLUB,
            DIAMOND,
            SPADE,
            HEART
        }
        private Suit suit;
        private int value;

        public Card(int value, Suit suit)
        {
            this.value = value;
            this.suit = suit;
        }

        public Suit getSuit()
        {
            return this.suit;

        }

        public int getValue()
        {
            return value;

        }

        public string toString()
        {
            return "Card{" + "suit=" + suit + ", value=" + value + '}';
        }

        public string ImagePath()
        {
            string path = "";
            string suitStr = "";
            switch (suit)
            {
                case Suit.CLUB:
                    suitStr = "C";
                    break;
                case Suit.DIAMOND:
                    suitStr = "D";
                    break;
                case Suit.SPADE:
                    suitStr = "S";
                    break;
                case Suit.HEART:
                    suitStr = "H";
                    break;
            }
            path = suitStr + value.ToString();
            return path;
        }

        public bool equals(object o)
        {
            if (this == o)
            {
                return true;
            }
            if (o == null || this.GetType() != o.GetType())
            {
                return false;
            }

            Card card = (Card)o;

            if (value != card.value)
            {
                return false;
            }
            return suit == card.suit;
        }
    }
}

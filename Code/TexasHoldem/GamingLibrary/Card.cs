using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaming
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

        public Suit getSuit() {
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

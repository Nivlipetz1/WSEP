using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Models
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
        private Suit suitCard;
        private int value;


        public Card(int value, Suit suit)
        {
            this.value = value;
            this.suitCard = suit;
        }

        public Suit SuitCard
        {
            get
            {
                return suitCard;
            }

            set
            {
                suitCard = value;
            }
        }

        public int Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }

        public string toImage()
        {
            switch (suitCard)
            {
                case Suit.CLUB:
                    return "C" + value + ".png";
                case Suit.SPADE:
                    return "S" + value + ".png";
                case Suit.HEART:
                    return "H" + value + ".png";
                case Suit.DIAMOND:
                    return "D" + value + ".png";
                default:
                    return "";
            }
        }


        public string toString()
        {
            return "Card{" + "suit=" + suitCard + ", value=" + value + '}';
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
            return suitCard == card.suitCard;
        }
    }
}

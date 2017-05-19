using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaming
{
    public class PlayerHand
    {
        private Card first;
        private Card second;

        public PlayerHand(Card first, Card second)
        {
            this.first = first;
            this.second = second;
        }

        public Card First
        {
            get
            {
                return first;
            }

            set
            {
                first = value;
            }
        }

        public Card Second
        {
            get
            {
                return second;
            }

            set
            {
                second = value;
            }
        }

        public string toString()
        {
            return first.toString() + second.toString();
        }
    }
}

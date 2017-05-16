
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Models
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

        public Card getFirst()
        {
            return first;
        }

        public Card getSecond()
        {
            return second;
        }

        public string toString()
        {
            return first.toString() + second.toString();
        }
    }
}

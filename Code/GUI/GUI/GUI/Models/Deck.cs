using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Models
{
    public class Deck
    {
        private List<Card> cards;

        public List<Card> Cards
        {
            get
            {
                return cards;
            }

            set
            {
                cards = value;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaming
{
    public class Deck
    {
        private List<Card> cards;

        public Deck() {
            this.cards = new List<Card>();
            foreach(Card.Suit suit in Enum.GetValues(typeof(Card.Suit))){
                for(int i=1;i<14;i++){
                    cards.Add(new Card(i,suit));
                }
            }
        }

        public List<Card> getCards()
        {
            return cards;
        }

        public void Shuffle()
        {
            FisherYates.Shuffle(cards);
        }

        public PlayerHand drawPlayerHand()
        {
            int index = new Random().Next(cards.Count);
            Card first = cards.ElementAt(index);
            cards.RemoveAt(index);
            index = new Random().Next(cards.Count);
            Card second = cards.ElementAt(index);
            cards.RemoveAt(index);
            return new PlayerHand(first, second);
        }

        public Card[] drawFlop()
        {
            Card[] flop = new Card[3];
            for (int i = 0; i < 3; i++)
            {
                int index = new Random().Next(cards.Count);
                flop[i] = cards.ElementAt(index);
                cards.RemoveAt(index);
            }
            return flop;

        }

        public Card drawRiver()
        {
            int index = new Random().Next(cards.Count);
            Card card = cards.ElementAt(index);
            cards.RemoveAt(index);
            return card;
        }

        public Card drawTurn()
        {
            int index = new Random().Next(cards.Count);
            Card card = cards.ElementAt(index);
            cards.RemoveAt(index);
            return card;
        }
    }
}

static public class FisherYates
{
    static Random r = new Random();
    //  Based on Java code from wikipedia:
    //  http://en.wikipedia.org/wiki/Fisher-Yates_shuffle
    static public void Shuffle(List<Gaming.Card> deck)
    {
        for (int n = deck.Count - 1; n > 0; --n)
        {
            int k = r.Next(n + 1);
            Gaming.Card temp = deck[n];
            deck[n] = deck[k];
            deck[k] = temp;
        }
    }
}

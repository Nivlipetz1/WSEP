using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Gaming;
using GameUtilities;

namespace GamingTests
{
    [TestFixture]
    public class CardAndDeckTests
    {

        [TestCase]
        public void CompareEqualCards()
        {
            Card c1 = new Card(1, Card.Suit.CLUB);
            Card c2 = new Card(1, Card.Suit.CLUB);
            Assert.True(c1.equals(c2));
            Assert.True(c1.equals(c1));
        }

        [TestCase]
        public void CompareUnequalCards()
        {
            Card c1 = new Card(1, Card.Suit.CLUB);
            Card c2 = new Card(1, Card.Suit.DIAMOND);
            Card c3 = new Card(10, Card.Suit.CLUB);
            Deck d = new Deck();
            Assert.False(c1.equals(c2));
            Assert.False(c2.equals(c3));
            Assert.False(c1.equals(d));

        
        }

        [TestCase]
        public void CreateDeckWithCorrectCards()
        {
            Deck d = new Deck();
            List<Card>cards = d.getCards();
            Card cardAtIndex9 = new Card(10,Card.Suit.CLUB);
            Card cardAtIndex51 = new Card(13,Card.Suit.HEART); //last card

            Assert.AreEqual(cards.ElementAt(9).getSuit(), cardAtIndex9.getSuit());
            Assert.AreEqual(cards.ElementAt(9).getValue(), cardAtIndex9.getValue());
            Assert.AreEqual(cards.ElementAt(51).getSuit(), cardAtIndex51.getSuit());
            Assert.AreEqual(cards.ElementAt(51).getValue(), cardAtIndex51.getValue());

        }

        [TestCase]
        public void ShuffleCards()
        {
            Deck d = new Deck();
            Assert.AreEqual(d.getCards().Count, 52);
            Card cardIndex24= d.getCards().ElementAt(24);
            d.Shuffle();
            Assert.AreEqual(d.getCards().Count, 52);
            Card cardAfterShuffleIndex24 = d.getCards().ElementAt(24);
            Assert.AreNotEqual(cardIndex24, cardAfterShuffleIndex24);
        }

        [TestCase]
        public void DrawPlayerHand()
        {
            Deck d = new Deck();
            PlayerHand ph = d.drawPlayerHand();
            Assert.False(d.getCards().Contains(ph.getFirst()));
            Assert.False(d.getCards().Contains(ph.getSecond()));
            Assert.AreNotEqual(ph.getFirst(), ph.getSecond());
            Assert.True(d.getCards().Count == 50);
            d.drawPlayerHand();
            Assert.True(d.getCards().Count == 48);
        }

        [TestCase]
        public void DrawFlop()
        {
            Deck d = new Deck();
            Card[] flop = d.drawFlop();
            Assert.True(flop.Count() == 3);
            Assert.True(d.getCards().Count == 49);
            for (int i = 0; i < 3; i++)
                Assert.False(d.getCards().Contains(flop[i]));
        }

        [TestCase]
        public void DrawRiver()
        {
            Deck d = new Deck();
            Card[] flop = d.drawFlop();
            Card river = d.DrawTableCard();
            Assert.True(d.getCards().Count == 48);
            Assert.False(flop.Contains(river));
            Assert.False(d.getCards().Contains(river));
        }

        [TestCase]
        public void DrawTurn()
        {
            Deck d = new Deck();
            Card[] flop = d.drawFlop();
            Card river = d.DrawTableCard();
            Card turn = d.DrawTableCard();
            Assert.True(d.getCards().Count == 47);
            Assert.False(flop.Contains(turn));
            Assert.AreNotEqual(river, turn);
            Assert.False(d.getCards().Contains(turn));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Gaming;

namespace GamingTests
{
    [TestFixture]
    public class CardAnalyzerTest
    {

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void hasRoyalFlushTest()
        public virtual void hasRoyalFlushTest()
        {
            Card[] cardsOnTable = new Card[]
            {
            new Card(10, Card.Suit.CLUB),
            new Card(11, Card.Suit.CLUB),
            new Card(12, Card.Suit.CLUB),
            new Card(13, Card.Suit.CLUB),
            new Card(8, Card.Suit.DIAMOND)
            };
            PlayerHand hand = new PlayerHand(new Card(6, Card.Suit.HEART), new Card(1, Card.Suit.CLUB));
            CardAnalyzer analyzer = new CardAnalyzer();
            analyzer.setCardArray(cardsOnTable);
            analyzer.setHand(hand);
            assertTrue(analyzer.RoyalFlush.operate() > 0);

        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void noRoyalFlushTest()
        public virtual void noRoyalFlushTest()
        {
            Card[] cardsOnTable = new Card[]
            {
            new Card(10, Card.Suit.CLUB),
            new Card(11, Card.Suit.CLUB),
            new Card(12, Card.Suit.HEART),
            new Card(13, Card.Suit.CLUB),
            new Card(8, Card.Suit.DIAMOND)
            };
            PlayerHand hand = new PlayerHand(new Card(6, Card.Suit.HEART), new Card(1, Card.Suit.CLUB));
            CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
            analyzer.Hand = hand;
            assertFalse(analyzer.RoyalFlush.operate() > 0);

        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void noRoyalFlush2Test()
        public virtual void noRoyalFlush2Test()
        {
            Card[] cardsOnTable = new Card[]
            {
            new Card(10, Card.Suit.CLUB),
            new Card(11, Card.Suit.CLUB),
            new Card(12, Card.Suit.CLUB),
            new Card(13, Card.Suit.CLUB),
            new Card(8, Card.Suit.DIAMOND)
            };
            PlayerHand hand = new PlayerHand(new Card(6, Card.Suit.HEART), new Card(7, Card.Suit.CLUB));
            CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
            analyzer.Hand = hand;
            assertFalse(analyzer.RoyalFlush.operate() > 0);

        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void noStraightFlushTest()
        public virtual void noStraightFlushTest()
        {
            Card[] cardsOnTable = new Card[]
            {
            new Card(10, Card.Suit.CLUB),
            new Card(11, Card.Suit.CLUB),
            new Card(12, Card.Suit.HEART),
            new Card(13, Card.Suit.CLUB),
            new Card(8, Card.Suit.DIAMOND)
            };
            PlayerHand hand = new PlayerHand(new Card(6, Card.Suit.HEART), new Card(1, Card.Suit.CLUB));
            CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
            analyzer.Hand = hand;
            assertFalse(analyzer.StraightFlush.operate() > 0);

        }
        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void hasStraightFlushTest()
        public virtual void hasStraightFlushTest()
        {
            Card[] cardsOnTable = new Card[]
            {
            new Card(4, Card.Suit.CLUB),
            new Card(5, Card.Suit.CLUB),
            new Card(12, Card.Suit.CLUB),
            new Card(8, Card.Suit.CLUB),
            new Card(8, Card.Suit.DIAMOND)
            };
            PlayerHand hand = new PlayerHand(new Card(6, Card.Suit.CLUB), new Card(7, Card.Suit.CLUB));
            CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
            analyzer.Hand = hand;
            assertTrue(analyzer.StraightFlush.operate() > 0);

        }
        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void hasStraightFlush2Test()
        public virtual void hasStraightFlush2Test()
        {
            Card[] cardsOnTable = new Card[]
            {
            new Card(4, Card.Suit.CLUB),
            new Card(5, Card.Suit.CLUB),
            new Card(6, Card.Suit.CLUB),
            new Card(8, Card.Suit.CLUB),
            new Card(8, Card.Suit.DIAMOND)
            };
            PlayerHand hand = new PlayerHand(new Card(9, Card.Suit.CLUB), new Card(7, Card.Suit.CLUB));
            CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
            analyzer.Hand = hand;
            assertTrue(analyzer.StraightFlush.operate() > 0);

        }
        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void hasFourOfAKindTest()
        public virtual void hasFourOfAKindTest()
        {
            Card[] cardsOnTable = new Card[]
            {
            new Card(4, Card.Suit.CLUB),
            new Card(5, Card.Suit.CLUB),
            new Card(12, Card.Suit.CLUB),
            new Card(8, Card.Suit.CLUB),
            new Card(8, Card.Suit.DIAMOND)
            };
            PlayerHand hand = new PlayerHand(new Card(8, Card.Suit.HEART), new Card(8, Card.Suit.SPADE));
            CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
            analyzer.Hand = hand;
            assertTrue(analyzer.FourOfAKind.operate() > 0);

        }
        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void noFourOfAKindTest()
        public virtual void noFourOfAKindTest()
        {
            Card[] cardsOnTable = new Card[]
            {
            new Card(4, Card.Suit.CLUB),
            new Card(5, Card.Suit.CLUB),
            new Card(12, Card.Suit.CLUB),
            new Card(8, Card.Suit.CLUB),
            new Card(8, Card.Suit.DIAMOND)
            };
            PlayerHand hand = new PlayerHand(new Card(6, Card.Suit.CLUB), new Card(7, Card.Suit.CLUB));
            CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
            analyzer.Hand = hand;
            assertFalse(analyzer.FourOfAKind.operate() > 0);

        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void hasFullHouseTest()
        public virtual void hasFullHouseTest()
        {
            Card[] cardsOnTable = new Card[]
            {
            new Card(10, Card.Suit.CLUB),
            new Card(10, Card.Suit.DIAMOND),
            new Card(13, Card.Suit.CLUB),
            new Card(8, Card.Suit.CLUB),
            new Card(5, Card.Suit.DIAMOND)
            };
            PlayerHand hand = new PlayerHand(new Card(10, Card.Suit.HEART), new Card(8, Card.Suit.SPADE));
            CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
            analyzer.Hand = hand;
            assertTrue(analyzer.FullHouse.operate() > 0);

        }
        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void noFullHouseTest()
        public virtual void noFullHouseTest()
        {
            Card[] cardsOnTable = new Card[]
            {
            new Card(4, Card.Suit.CLUB),
            new Card(5, Card.Suit.CLUB),
            new Card(4, Card.Suit.HEART),
            new Card(4, Card.Suit.DIAMOND),
            new Card(6, Card.Suit.HEART)
            };
            PlayerHand hand = new PlayerHand(new Card(7, Card.Suit.CLUB), new Card(8, Card.Suit.CLUB));
            CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
            analyzer.Hand = hand;
            assertFalse(analyzer.FullHouse.operate() > 0);

        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void hasFlushTest()
        public virtual void hasFlushTest()
        {
            Card[] cardsOnTable = new Card[]
            {
            new Card(10, Card.Suit.CLUB),
            new Card(10, Card.Suit.DIAMOND),
            new Card(13, Card.Suit.CLUB),
            new Card(8, Card.Suit.CLUB),
            new Card(5, Card.Suit.DIAMOND)
            };
            PlayerHand hand = new PlayerHand(new Card(7, Card.Suit.CLUB), new Card(2, Card.Suit.CLUB));
            CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
            analyzer.Hand = hand;
            assertTrue(analyzer.Flush.operate() > 0);

        }
        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void noFlushTest()
        public virtual void noFlushTest()
        {
            Card[] cardsOnTable = new Card[]
            {
            new Card(4, Card.Suit.CLUB),
            new Card(9, Card.Suit.CLUB),
            new Card(7, Card.Suit.HEART),
            new Card(2, Card.Suit.CLUB),
            new Card(6, Card.Suit.HEART)
            };
            PlayerHand hand = new PlayerHand(new Card(13, Card.Suit.CLUB), new Card(4, Card.Suit.DIAMOND));
            CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
            analyzer.Hand = hand;
            assertFalse(analyzer.Flush.operate() > 0);

        }
        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void hasStraightTest()
        public virtual void hasStraightTest()
        {
            Card[] cardsOnTable = new Card[]
            {
            new Card(4, Card.Suit.CLUB),
            new Card(12, Card.Suit.CLUB),
            new Card(7, Card.Suit.HEART),
            new Card(2, Card.Suit.CLUB),
            new Card(6, Card.Suit.HEART)
            };
            PlayerHand hand = new PlayerHand(new Card(5, Card.Suit.CLUB), new Card(8, Card.Suit.DIAMOND));
            CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
            analyzer.Hand = hand;
            assertTrue(analyzer.Straight.operate() > 0);

        }
        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void noStraightTest()
        public virtual void noStraightTest()
        {
            Card[] cardsOnTable = new Card[]
            {
            new Card(4, Card.Suit.CLUB),
            new Card(12, Card.Suit.CLUB),
            new Card(7, Card.Suit.HEART),
            new Card(2, Card.Suit.CLUB),
            new Card(6, Card.Suit.HEART)
            };
            PlayerHand hand = new PlayerHand(new Card(5, Card.Suit.CLUB), new Card(5, Card.Suit.DIAMOND));
            CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
            analyzer.Hand = hand;
            assertFalse(analyzer.Straight.operate() > 0);

        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void hasThreeOfAKindTest()
        public virtual void hasThreeOfAKindTest()
        {
            Card[] cardsOnTable = new Card[]
            {
            new Card(4, Card.Suit.CLUB),
            new Card(5, Card.Suit.CLUB),
            new Card(12, Card.Suit.CLUB),
            new Card(8, Card.Suit.CLUB),
            new Card(8, Card.Suit.DIAMOND)
            };
            PlayerHand hand = new PlayerHand(new Card(8, Card.Suit.HEART), new Card(10, Card.Suit.SPADE));
            CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
            analyzer.Hand = hand;
            assertTrue(analyzer.ThreeOfAKind.operate() > 0);

        }
        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void noThreeOfAKindTest()
        public virtual void noThreeOfAKindTest()
        {
            Card[] cardsOnTable = new Card[]
            {
            new Card(4, Card.Suit.CLUB),
            new Card(5, Card.Suit.CLUB),
            new Card(12, Card.Suit.CLUB),
            new Card(2, Card.Suit.CLUB),
            new Card(8, Card.Suit.DIAMOND)
            };
            PlayerHand hand = new PlayerHand(new Card(8, Card.Suit.CLUB), new Card(7, Card.Suit.CLUB));
            CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
            analyzer.Hand = hand;
            assertFalse(analyzer.ThreeOfAKind.operate() > 0);

        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void hasTwoPairTest()
        public virtual void hasTwoPairTest()
        {
            Card[] cardsOnTable = new Card[]
            {
            new Card(4, Card.Suit.CLUB),
            new Card(5, Card.Suit.CLUB),
            new Card(10, Card.Suit.CLUB),
            new Card(8, Card.Suit.CLUB),
            new Card(8, Card.Suit.DIAMOND)
            };
            PlayerHand hand = new PlayerHand(new Card(9, Card.Suit.HEART), new Card(10, Card.Suit.SPADE));
            CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
            analyzer.Hand = hand;
            assertTrue(analyzer.TwoPair.operate() > 0);

        }
        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void noTwoPairTest()
        public virtual void noTwoPairTest()
        {
            Card[] cardsOnTable = new Card[]
            {
            new Card(4, Card.Suit.CLUB),
            new Card(5, Card.Suit.CLUB),
            new Card(12, Card.Suit.CLUB),
            new Card(2, Card.Suit.CLUB),
            new Card(8, Card.Suit.DIAMOND)
            };
            PlayerHand hand = new PlayerHand(new Card(8, Card.Suit.CLUB), new Card(7, Card.Suit.CLUB));
            CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
            analyzer.Hand = hand;
            assertFalse(analyzer.TwoPair.operate() > 0);

        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void highCardAnalyzeTest()
        public virtual void highCardAnalyzeTest()
        {
            Card[] cardsOnTable = new Card[]
            {
            new Card(4, Card.Suit.CLUB),
            new Card(3, Card.Suit.CLUB),
            new Card(12, Card.Suit.CLUB),
            new Card(2, Card.Suit.CLUB),
            new Card(6, Card.Suit.DIAMOND)
            };
            PlayerHand hand = new PlayerHand(new Card(8, Card.Suit.CLUB), new Card(7, Card.Suit.CLUB));
            CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
            analyzer.Hand = hand;
            assertEquals(CardAnalyzer.HandRank.HighCard, analyzer.analyze());

        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void fullHouseAnalyzeTest()
        public virtual void fullHouseAnalyzeTest()
        {
            Card[] cardsOnTable = new Card[]
            {
            new Card(4, Card.Suit.CLUB),
            new Card(3, Card.Suit.CLUB),
            new Card(12, Card.Suit.CLUB),
            new Card(2, Card.Suit.CLUB),
            new Card(4, Card.Suit.DIAMOND)
            };
            PlayerHand hand = new PlayerHand(new Card(4, Card.Suit.HEART), new Card(2, Card.Suit.HEART));
            CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
            analyzer.Hand = hand;
            assertEquals(CardAnalyzer.HandRank.FullHouse, analyzer.analyze());

        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void lowStraightAnalyzeTest()
        public virtual void lowStraightAnalyzeTest()
        {
            Card[] cardsOnTable = new Card[]
            {
            new Card(1, Card.Suit.CLUB),
            new Card(3, Card.Suit.CLUB),
            new Card(12, Card.Suit.CLUB),
            new Card(2, Card.Suit.CLUB),
            new Card(4, Card.Suit.DIAMOND)
            };
            PlayerHand hand = new PlayerHand(new Card(5, Card.Suit.HEART), new Card(2, Card.Suit.HEART));
            CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
            analyzer.Hand = hand;
            assertEquals(CardAnalyzer.HandRank.Straight, analyzer.analyze());

        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void highStraightAnalyzeTest()
        public virtual void highStraightAnalyzeTest()
        {
            Card[] cardsOnTable = new Card[]
            {
            new Card(1, Card.Suit.CLUB),
            new Card(3, Card.Suit.CLUB),
            new Card(12, Card.Suit.CLUB),
            new Card(2, Card.Suit.CLUB),
            new Card(11, Card.Suit.DIAMOND)
            };
            PlayerHand hand = new PlayerHand(new Card(10, Card.Suit.HEART), new Card(13, Card.Suit.HEART));
            CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
            analyzer.Hand = hand;
            assertEquals(CardAnalyzer.HandRank.Straight, analyzer.analyze());

        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void pairTieBreakerTest()
        public virtual void pairTieBreakerTest()
        {
            Card[] cardsOnTable = new Card[]
            {
            new Card(1, Card.Suit.CLUB),
            new Card(3, Card.Suit.CLUB),
            new Card(12, Card.Suit.CLUB),
            new Card(2, Card.Suit.CLUB),
            new Card(11, Card.Suit.DIAMOND)
            };
            PlayerHand hand1 = new PlayerHand(new Card(11, Card.Suit.HEART), new Card(13, Card.Suit.HEART));
            PlayerHand hand2 = new PlayerHand(new Card(2, Card.Suit.HEART), new Card(5, Card.Suit.HEART));
            CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
            assertEquals(hand1, analyzer.tieBreaker(CardAnalyzer.HandRank.OnePair, hand1, hand2));

        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void straightTieBreakerTest()
        public virtual void straightTieBreakerTest()
        {
            Card[] cardsOnTable = new Card[]
            {
            new Card(1, Card.Suit.CLUB),
            new Card(5, Card.Suit.CLUB),
            new Card(4, Card.Suit.CLUB),
            new Card(2, Card.Suit.SPADE),
            new Card(11, Card.Suit.DIAMOND)
            };
            PlayerHand hand1 = new PlayerHand(new Card(3, Card.Suit.HEART), new Card(13, Card.Suit.HEART));
            PlayerHand hand2 = new PlayerHand(new Card(6, Card.Suit.HEART), new Card(3, Card.Suit.CLUB));
            CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
            assertEquals(hand2, analyzer.tieBreaker(CardAnalyzer.HandRank.Straight, hand1, hand2));

        }
    }

}
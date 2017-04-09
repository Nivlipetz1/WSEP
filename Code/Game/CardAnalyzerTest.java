/**
 * Created by omerh on 4/8/2017.
 */

import static org.junit.Assert.*;
import org.junit.Test;

public class CardAnalyzerTest {

    @Test
    public void hasRoyalFlushTest(){
        Card[] cardsOnTable = new Card[]{new Card(10, Card.Suit.CLUB),
                                         new Card(11, Card.Suit.CLUB),
                                         new Card(12, Card.Suit.CLUB),
                                         new Card(13, Card.Suit.CLUB),
                                         new Card(8, Card.Suit.DIAMOND),
        };
        PlayerHand hand = new PlayerHand(new Card(6, Card.Suit.HEART),new Card(1, Card.Suit.CLUB));
        CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
        analyzer.setHand(hand);
        assertTrue(analyzer.RoyalFlush.operate()>0);

    }

    @Test
    public void noRoyalFlushTest(){
        Card[] cardsOnTable = new Card[]{new Card(10, Card.Suit.CLUB),
                new Card(11, Card.Suit.CLUB),
                new Card(12, Card.Suit.HEART),
                new Card(13, Card.Suit.CLUB),
                new Card(8, Card.Suit.DIAMOND),
        };
        PlayerHand hand = new PlayerHand(new Card(6, Card.Suit.HEART),new Card(1, Card.Suit.CLUB));
        CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
        analyzer.setHand(hand);
        assertFalse(analyzer.RoyalFlush.operate()>0);

    }

    @Test
    public void noRoyalFlush2Test(){
        Card[] cardsOnTable = new Card[]{new Card(10, Card.Suit.CLUB),
                new Card(11, Card.Suit.CLUB),
                new Card(12, Card.Suit.CLUB),
                new Card(13, Card.Suit.CLUB),
                new Card(8, Card.Suit.DIAMOND),
        };
        PlayerHand hand = new PlayerHand(new Card(6, Card.Suit.HEART),new Card(7, Card.Suit.CLUB));
        CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
        analyzer.setHand(hand);
        assertFalse(analyzer.RoyalFlush.operate()>0);

    }

    @Test
    public void noStraightFlushTest(){
        Card[] cardsOnTable = new Card[]{new Card(10, Card.Suit.CLUB),
                new Card(11, Card.Suit.CLUB),
                new Card(12, Card.Suit.HEART),
                new Card(13, Card.Suit.CLUB),
                new Card(8, Card.Suit.DIAMOND),
        };
        PlayerHand hand = new PlayerHand(new Card(6, Card.Suit.HEART),new Card(1, Card.Suit.CLUB));
        CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
        analyzer.setHand(hand);
        assertFalse(analyzer.StraightFlush.operate()>0);

    }
    @Test
    public void hasStraightFlushTest(){
        Card[] cardsOnTable = new Card[]{new Card(4, Card.Suit.CLUB),
                new Card(5, Card.Suit.CLUB),
                new Card(12, Card.Suit.CLUB),
                new Card(8, Card.Suit.CLUB),
                new Card(8, Card.Suit.DIAMOND),
        };
        PlayerHand hand = new PlayerHand(new Card(6, Card.Suit.CLUB),new Card(7, Card.Suit.CLUB));
        CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
        analyzer.setHand(hand);
        assertTrue(analyzer.StraightFlush.operate()>0);

    }
    @Test
    public void hasStraightFlush2Test(){
        Card[] cardsOnTable = new Card[]{new Card(4, Card.Suit.CLUB),
                new Card(5, Card.Suit.CLUB),
                new Card(6, Card.Suit.CLUB),
                new Card(8, Card.Suit.CLUB),
                new Card(8, Card.Suit.DIAMOND),
        };
        PlayerHand hand = new PlayerHand(new Card(9, Card.Suit.CLUB),new Card(7, Card.Suit.CLUB));
        CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
        analyzer.setHand(hand);
        assertTrue(analyzer.StraightFlush.operate()>0);

    }
    @Test
    public void hasFourOfAKindTest(){
        Card[] cardsOnTable = new Card[]{new Card(4, Card.Suit.CLUB),
                new Card(5, Card.Suit.CLUB),
                new Card(12, Card.Suit.CLUB),
                new Card(8, Card.Suit.CLUB),
                new Card(8, Card.Suit.DIAMOND),
        };
        PlayerHand hand = new PlayerHand(new Card(8, Card.Suit.HEART),new Card(8, Card.Suit.SPADE));
        CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
        analyzer.setHand(hand);
        assertTrue(analyzer.FourOfAKind.operate()>0);

    }
    @Test
    public void noFourOfAKindTest(){
        Card[] cardsOnTable = new Card[]{new Card(4, Card.Suit.CLUB),
                new Card(5, Card.Suit.CLUB),
                new Card(12, Card.Suit.CLUB),
                new Card(8, Card.Suit.CLUB),
                new Card(8, Card.Suit.DIAMOND),
        };
        PlayerHand hand = new PlayerHand(new Card(6, Card.Suit.CLUB),new Card(7, Card.Suit.CLUB));
        CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
        analyzer.setHand(hand);
        assertFalse(analyzer.FourOfAKind.operate()>0);

    }

    @Test
    public void hasFullHouseTest(){
        Card[] cardsOnTable = new Card[]{new Card(10, Card.Suit.CLUB),
                new Card(10, Card.Suit.DIAMOND),
                new Card(13, Card.Suit.CLUB),
                new Card(8, Card.Suit.CLUB),
                new Card(5, Card.Suit.DIAMOND),
        };
        PlayerHand hand = new PlayerHand(new Card(10, Card.Suit.HEART),new Card(8, Card.Suit.SPADE));
        CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
        analyzer.setHand(hand);
        assertTrue(analyzer.FullHouse.operate()>0);

    }
    @Test
    public void noFullHouseTest(){
        Card[] cardsOnTable = new Card[]{new Card(4, Card.Suit.CLUB),
                new Card(5, Card.Suit.CLUB),
                new Card(4, Card.Suit.HEART),
                new Card(4, Card.Suit.DIAMOND),
                new Card(6, Card.Suit.HEART),
        };
        PlayerHand hand = new PlayerHand(new Card(7, Card.Suit.CLUB),new Card(8, Card.Suit.CLUB));
        CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
        analyzer.setHand(hand);
        assertFalse(analyzer.FullHouse.operate()>0);

    }

    @Test
    public void hasFlushTest(){
        Card[] cardsOnTable = new Card[]{new Card(10, Card.Suit.CLUB),
                new Card(10, Card.Suit.DIAMOND),
                new Card(13, Card.Suit.CLUB),
                new Card(8, Card.Suit.CLUB),
                new Card(5, Card.Suit.DIAMOND),
        };
        PlayerHand hand = new PlayerHand(new Card(7, Card.Suit.CLUB),new Card(2, Card.Suit.CLUB));
        CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
        analyzer.setHand(hand);
        assertTrue(analyzer.Flush.operate()>0);

    }
    @Test
    public void noFlushTest(){
        Card[] cardsOnTable = new Card[]{new Card(4, Card.Suit.CLUB),
                new Card(9, Card.Suit.CLUB),
                new Card(7, Card.Suit.HEART),
                new Card(2, Card.Suit.CLUB),
                new Card(6, Card.Suit.HEART),
        };
        PlayerHand hand = new PlayerHand(new Card(13, Card.Suit.CLUB),new Card(4, Card.Suit.DIAMOND));
        CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
        analyzer.setHand(hand);
        assertFalse(analyzer.Flush.operate()>0);

    }
    @Test
    public void hasStraightTest(){
        Card[] cardsOnTable = new Card[]{new Card(4, Card.Suit.CLUB),
                new Card(12, Card.Suit.CLUB),
                new Card(7, Card.Suit.HEART),
                new Card(2, Card.Suit.CLUB),
                new Card(6, Card.Suit.HEART),
        };
        PlayerHand hand = new PlayerHand(new Card(5, Card.Suit.CLUB),new Card(8, Card.Suit.DIAMOND));
        CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
        analyzer.setHand(hand);
        assertTrue(analyzer.Straight.operate()>0);

    }
    @Test
    public void noStraightTest(){
        Card[] cardsOnTable = new Card[]{new Card(4, Card.Suit.CLUB),
                new Card(12, Card.Suit.CLUB),
                new Card(7, Card.Suit.HEART),
                new Card(2, Card.Suit.CLUB),
                new Card(6, Card.Suit.HEART),
        };
        PlayerHand hand = new PlayerHand(new Card(5, Card.Suit.CLUB),new Card(5, Card.Suit.DIAMOND));
        CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
        analyzer.setHand(hand);
        assertFalse(analyzer.Straight.operate()>0);

    }

    @Test
    public void hasThreeOfAKindTest(){
        Card[] cardsOnTable = new Card[]{new Card(4, Card.Suit.CLUB),
                new Card(5, Card.Suit.CLUB),
                new Card(12, Card.Suit.CLUB),
                new Card(8, Card.Suit.CLUB),
                new Card(8, Card.Suit.DIAMOND),
        };
        PlayerHand hand = new PlayerHand(new Card(8, Card.Suit.HEART),new Card(10, Card.Suit.SPADE));
        CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
        analyzer.setHand(hand);
        assertTrue(analyzer.ThreeOfAKind.operate()>0);

    }
    @Test
    public void noThreeOfAKindTest(){
        Card[] cardsOnTable = new Card[]{new Card(4, Card.Suit.CLUB),
                new Card(5, Card.Suit.CLUB),
                new Card(12, Card.Suit.CLUB),
                new Card(2, Card.Suit.CLUB),
                new Card(8, Card.Suit.DIAMOND),
        };
        PlayerHand hand = new PlayerHand(new Card(8, Card.Suit.CLUB),new Card(7, Card.Suit.CLUB));
        CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
        analyzer.setHand(hand);
        assertFalse(analyzer.ThreeOfAKind.operate()>0);

    }

    @Test
    public void hasTwoPairTest(){
        Card[] cardsOnTable = new Card[]{new Card(4, Card.Suit.CLUB),
                new Card(5, Card.Suit.CLUB),
                new Card(10, Card.Suit.CLUB),
                new Card(8, Card.Suit.CLUB),
                new Card(8, Card.Suit.DIAMOND),
        };
        PlayerHand hand = new PlayerHand(new Card(9, Card.Suit.HEART),new Card(10, Card.Suit.SPADE));
        CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
        analyzer.setHand(hand);
        assertTrue(analyzer.TwoPair.operate()>0);

    }
    @Test
    public void noTwoPairTest(){
        Card[] cardsOnTable = new Card[]{new Card(4, Card.Suit.CLUB),
                new Card(5, Card.Suit.CLUB),
                new Card(12, Card.Suit.CLUB),
                new Card(2, Card.Suit.CLUB),
                new Card(8, Card.Suit.DIAMOND),
        };
        PlayerHand hand = new PlayerHand(new Card(8, Card.Suit.CLUB),new Card(7, Card.Suit.CLUB));
        CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
        analyzer.setHand(hand);
        assertFalse(analyzer.TwoPair.operate()>0);

    }

    @Test
    public void highCardAnalyzeTest(){
        Card[] cardsOnTable = new Card[]{new Card(4, Card.Suit.CLUB),
                new Card(3, Card.Suit.CLUB),
                new Card(12, Card.Suit.CLUB),
                new Card(2, Card.Suit.CLUB),
                new Card(6, Card.Suit.DIAMOND),
        };
        PlayerHand hand = new PlayerHand(new Card(8, Card.Suit.CLUB),new Card(7, Card.Suit.CLUB));
        CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
        analyzer.setHand(hand);
        assertEquals(CardAnalyzer.HandRank.HighCard,analyzer.analyze());

    }

    @Test
    public void fullHouseAnalyzeTest(){
        Card[] cardsOnTable = new Card[]{new Card(4, Card.Suit.CLUB),
                new Card(3, Card.Suit.CLUB),
                new Card(12, Card.Suit.CLUB),
                new Card(2, Card.Suit.CLUB),
                new Card(4, Card.Suit.DIAMOND),
        };
        PlayerHand hand = new PlayerHand(new Card(4, Card.Suit.HEART),new Card(2, Card.Suit.HEART));
        CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
        analyzer.setHand(hand);
        assertEquals(CardAnalyzer.HandRank.FullHouse,analyzer.analyze());

    }

    @Test
    public void lowStraightAnalyzeTest(){
        Card[] cardsOnTable = new Card[]{new Card(1, Card.Suit.CLUB),
                new Card(3, Card.Suit.CLUB),
                new Card(12, Card.Suit.CLUB),
                new Card(2, Card.Suit.CLUB),
                new Card(4, Card.Suit.DIAMOND),
        };
        PlayerHand hand = new PlayerHand(new Card(5, Card.Suit.HEART),new Card(2, Card.Suit.HEART));
        CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
        analyzer.setHand(hand);
        assertEquals(CardAnalyzer.HandRank.Straight,analyzer.analyze());

    }

    @Test
    public void highStraightAnalyzeTest(){
        Card[] cardsOnTable = new Card[]{new Card(1, Card.Suit.CLUB),
                new Card(3, Card.Suit.CLUB),
                new Card(12, Card.Suit.CLUB),
                new Card(2, Card.Suit.CLUB),
                new Card(11, Card.Suit.DIAMOND),
        };
        PlayerHand hand = new PlayerHand(new Card(10, Card.Suit.HEART),new Card(13, Card.Suit.HEART));
        CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
        analyzer.setHand(hand);
        assertEquals(CardAnalyzer.HandRank.Straight,analyzer.analyze());

    }

    @Test
    public void pairTieBreakerTest(){
        Card[] cardsOnTable = new Card[]{new Card(1, Card.Suit.CLUB),
                new Card(3, Card.Suit.CLUB),
                new Card(12, Card.Suit.CLUB),
                new Card(2, Card.Suit.CLUB),
                new Card(11, Card.Suit.DIAMOND),
        };
        PlayerHand hand1 = new PlayerHand(new Card(11, Card.Suit.HEART),new Card(13, Card.Suit.HEART));
        PlayerHand hand2 = new PlayerHand(new Card(2, Card.Suit.HEART),new Card(5, Card.Suit.HEART));
        CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
        assertEquals(hand1,analyzer.tieBreaker(CardAnalyzer.HandRank.OnePair,hand1, hand2));

    }

    @Test
    public void straightTieBreakerTest(){
        Card[] cardsOnTable = new Card[]{new Card(1, Card.Suit.CLUB),
                new Card(5, Card.Suit.CLUB),
                new Card(4, Card.Suit.CLUB),
                new Card(2, Card.Suit.SPADE),
                new Card(11, Card.Suit.DIAMOND),
        };
        PlayerHand hand1 = new PlayerHand(new Card(3, Card.Suit.HEART),new Card(13, Card.Suit.HEART));
        PlayerHand hand2 = new PlayerHand(new Card(6, Card.Suit.HEART),new Card(3, Card.Suit.CLUB));
        CardAnalyzer analyzer = new CardAnalyzer(cardsOnTable);
        assertEquals(hand2,analyzer.tieBreaker(CardAnalyzer.HandRank.Straight,hand1, hand2));

    }
}

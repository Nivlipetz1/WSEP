import javax.jws.Oneway;
import java.util.HashMap;
import java.util.Map;

/**
 * Created by omerh on 4/8/2017.
 */



public class CardAnalyzer {
    public static final int ACE = 13;
    public enum HandRank { RoyalFlush, StraightFlush, FourOfAKind,
                           FullHouse, Flush, Straight, ThreeOfAKind,
                           TwoPair, OnePair, HighCard
                            }



    private Card[] cardArray;
    private PlayerHand hand;
    private Map<HandRank,AnalyzeOp> map = new HashMap();

    public CardAnalyzer(Card[] cardArray) {
        this.cardArray = cardArray;
        map.put(HandRank.RoyalFlush,RoyalFlush);
        map.put(HandRank.StraightFlush,StraightFlush);
        map.put(HandRank.FourOfAKind,FourOfAKind);
        map.put(HandRank.FullHouse,FullHouse);
        map.put(HandRank.Flush,Flush);
        map.put(HandRank.Straight,Straight);
        map.put(HandRank.ThreeOfAKind,ThreeOfAKind);
        map.put(HandRank.TwoPair,TwoPair);
        map.put(HandRank.OnePair, OnePair);
        map.put(HandRank.HighCard,HighCard);
    }

    public void setHand(PlayerHand hand) {
        this.hand = hand;
    }

    public HandRank analyze(){
        for(HandRank rank : HandRank.values()) {
            if (map.get(rank).operate()) {
                return rank;
            }
        }
        return null;//Should Never Get Here!!
    }

    AnalyzeOp RoyalFlush = () -> {
        boolean breakLoop = false;
        for(Card.Suit suit : Card.Suit.values()) {
            for (int value = 10; value <= ACE; value++) {
                if (!CheckIfCardExists(new Card(value, suit))) {
                    breakLoop = true;
                    break;
                }
            }
            if(!CheckIfCardExists(new Card(1,suit)))
                breakLoop = true;
            if(!breakLoop)
                return true;
        }
        return false;
    };

    AnalyzeOp StraightFlush = () ->{
        for(Card.Suit suit : Card.Suit.values()) {
            int straightInARow = 0;
            for (int value = 1; value <= ACE; value++) {
                if (!CheckIfCardExists(new Card(value, suit))) {
                    straightInARow=0;
                }
                else{
                    straightInARow++;
                    if(straightInARow>4){
                        return true;
                    }
                }
            }
            if(CheckIfCardExists(new Card(1,suit)) && straightInARow==4)
                return true;

        }
        //System.out.println(straightInARow);
        return false;
    };

    AnalyzeOp FourOfAKind = () -> {
        int[] instanceCounter = countInstancesByValue();
        for(int instances : instanceCounter){
            if (instances==4){
                return true;
            }
        }
        return false;
    };

    AnalyzeOp FullHouse = () -> {
        int[] instanceCounter = countInstancesByValue();
        for(int i=0;i<instanceCounter.length;i++){
            if (instanceCounter[i]>=3){
                for(int j=0;j<instanceCounter.length;j++){
                    if(instanceCounter[j]>=2 && i!=j)
                        return true;
                }
            }
        }
        return false;
    };

    AnalyzeOp Flush = () -> {
        int[] instanceCounter = countInstancesBySuit();
        for(int instances : instanceCounter){
            if (instances==5){
                return true;
            }
        }
        return false;
    };

    AnalyzeOp Straight = () -> {
        int straightInARow = 0;
        for (int value = 1; value <= ACE; value++) {
            boolean valueAppeared = false;
            for (Card.Suit suit : Card.Suit.values()) {
                if (CheckIfCardExists(new Card(value, suit)))
                    valueAppeared = true;
            }
            if (valueAppeared) {
                straightInARow++;
                //System.out.println("increased int to "+straightInARow);
                if(straightInARow>=5)
                    return true;
            }
            else {
                straightInARow = 0;
                //System.out.println("reset int");
            }
        }
        for (Card.Suit suit : Card.Suit.values()) {
            if (CheckIfCardExists(new Card(1, suit)) && straightInARow == 4)
                return true;
        }
        return false;
    };

    AnalyzeOp ThreeOfAKind = () -> {
        int[] instanceCounter = countInstancesByValue();
        for(int instances : instanceCounter){
            if (instances>=3){
                return true;
            }
        }
        return false;
    };

    AnalyzeOp TwoPair = () -> {
        int[] instanceCounter = countInstancesByValue();
        for(int i=0;i<instanceCounter.length;i++){
            if (instanceCounter[i]>=2){
                for(int j=0;j<instanceCounter.length;j++){
                    if(instanceCounter[j]>=2 && i!=j)
                        return true;
                }
            }
        }
        return false;
    };

    AnalyzeOp OnePair = () -> {
        int[] instanceCounter = countInstancesByValue();
        for(int instances : instanceCounter){
            if (instances>=2){
                return true;
            }
        }
        return false;
    };

    AnalyzeOp HighCard = () -> {
        return true;
    };

    private int[] countInstancesByValue(){
        int[] instanceCounter = new int[15];
        for (Card.Suit suit : Card.Suit.values()) {
            for (int value = 1; value <= ACE; value++) {
                if (CheckIfCardExists(new Card(value, suit)))
                    instanceCounter[value]++;
            }
        }
        return instanceCounter;
    }

    private int[] countInstancesBySuit(){
        int[] instanceCounter = new int[4];
        for (Card.Suit suit : Card.Suit.values()) {
            for (int value = 1; value <= ACE; value++) {
                if (CheckIfCardExists(new Card(value, suit)))
                    instanceCounter[suit.ordinal()]++;
            }
        }
        return instanceCounter;
    }







    protected boolean CheckIfCardExists(Card card){
    for(Card cardFromTable : cardArray){
        if(card.equals(cardFromTable))
            return true;
    }
    if (card.equals(hand.getFirst()))
        return true;
    if (card.equals(hand.getSecond()))
        return true;

    //System.out.println("Card "+card.getValue()+" of Suite "+card.getSuit()+ " doesnt exist");
    return false;
}

}

interface AnalyzeOp{
    boolean operate();
}

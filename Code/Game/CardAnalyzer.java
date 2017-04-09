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
            if (map.get(rank).operate()!=0) {
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
                return 10;
        }
        return 0;
    };

    AnalyzeOp StraightFlush = () ->{
        int return_value = 0;
        boolean hasStraight = false;
        for(Card.Suit suit : Card.Suit.values()) {
            int straightInARow = 0;
            for (int value = 1; value <= ACE; value++) {
                if (!CheckIfCardExists(new Card(value, suit))) {
                    straightInARow=0;
                }
                else{
                    if(straightInARow<5)
                        straightInARow++;
                    if(straightInARow>=5){
                        hasStraight = true;
                        return_value = value;
                    }
                }
            }
            if(CheckIfCardExists(new Card(1,suit)) && straightInARow==4)
                return 1;

        }
        //System.out.println(return_value);
        if(hasStraight)
            return return_value;
        else
            return 0;
    };

    AnalyzeOp FourOfAKind = () -> {
        int[] instanceCounter = countInstancesByValue();
        int i=0;
        for(int instances : instanceCounter){
            i++;
            if (instances==4){
                return i;
            }
        }
        return 0;
    };

    AnalyzeOp FullHouse = () -> {
        int[] instanceCounter = countInstancesByValue();
        for(int i=0;i<instanceCounter.length;i++){
            if (instanceCounter[i]>=3){
                for(int j=0;j<instanceCounter.length;j++){
                    if(instanceCounter[j]>=2 && i!=j)
                        return i;
                }
            }
        }
        return 0;
    };

    AnalyzeOp Flush = () -> {
        int[] instanceCounter = countInstancesBySuit();
        for(int instances : instanceCounter){
            if (instances==5){
                return 10;
            }
        }
        return 0;
    };

    AnalyzeOp Straight = () -> {
        int straightInARow = 0;
        int return_value = 0;
        boolean hasStraight = false;
        for (int value = 1; value <= ACE; value++) {
            boolean valueAppeared = false;
            for (Card.Suit suit : Card.Suit.values()) {
                if (CheckIfCardExists(new Card(value, suit)))
                    valueAppeared = true;
            }
            if (valueAppeared) {
                if(straightInARow<5)
                    straightInARow++;
                if(straightInARow==5)
                    return_value = value;
                //System.out.println("increased int to "+straightInARow);
                if(straightInARow>=5)
                    hasStraight = true;
            }
            else {
                straightInARow = 0;
                //System.out.println("reset int");
            }
        }
        for (Card.Suit suit : Card.Suit.values()) {
            if (CheckIfCardExists(new Card(1, suit)) && straightInARow >= 4)
                return 1;
        }
        if(hasStraight) {
            //System.out.println("im returning "+return_value);
            return return_value;
        }
        else
            return 0;
    };

    AnalyzeOp ThreeOfAKind = () -> {
        int[] instanceCounter = countInstancesByValue();
        int i=0;
        for(int instances : instanceCounter){
            i++;
            if (instances>=3){
                return i;
            }
        }
        return 0;
    };

    AnalyzeOp TwoPair = () -> {
        int[] instanceCounter = countInstancesByValue();
        for(int i=0;i<instanceCounter.length;i++){
            if (instanceCounter[i]>=2){
                for(int j=0;j<instanceCounter.length;j++){
                    if(instanceCounter[j]>=2 && i!=j)

                        return i>j ? i : j;
                }
            }
        }
        return 0;
    };

    AnalyzeOp OnePair = () -> {
        int[] instanceCounter = countInstancesByValue();
        int i=0;
        for(int instances : instanceCounter){
            i++;
            if (instances>=2){
                return i;
            }

        }
        return 0;
    };

    AnalyzeOp HighCard = () -> {
        int[] instances = countInstancesByValue();
        for(int i=instances.length-1;i>=0;i--) {
            if (instances[i] > 0) {
                return i;
            }
        }
        return 0;//Should Never Get Here!!
    };

    public PlayerHand tieBreaker(HandRank rank,PlayerHand hand1, PlayerHand hand2){
        this.setHand(hand1);
        int hand1rank = map.get(rank).operate();
        this.setHand(hand2);
        int hand2rank = map.get(rank).operate();
        if(hand1rank>hand2rank)
            return hand1;
        else if(hand2rank>hand1rank)
            return hand2;
        else
            return null;
    }

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
    int operate();
}

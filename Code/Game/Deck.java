import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Random;

/**
 * Created by omerh on 4/9/2017.
 */
public class Deck {
    List<Card> cards;

    public Deck() {
        this.cards = new ArrayList<Card>();
        for(Card.Suit suit : Card.Suit.values()){
            for(int i=1;i<14;i++){
                cards.add(new Card(i,suit));
            }
        }
    }

    public List<Card> getCards() {
        return cards;
    }

    public void Shuffle(){
        Collections.shuffle(cards);
    }

    public PlayerHand drawPlayerHand(){
        int index = new Random().nextInt(cards.size());
        Card first = cards.remove(index);
        index = new Random().nextInt(cards.size());
        Card second = cards.remove(index);
        return new PlayerHand(first,second);

    }
}

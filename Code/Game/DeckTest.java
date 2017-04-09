/**
 * Created by omerh on 4/9/2017.
 */
import static org.junit.Assert.*;

import org.hamcrest.collection.IsIterableContainingInOrder;
import org.junit.Assert;
import org.junit.Test;
import org.hamcrest.collection.*;
import static org.hamcrest.Matchers.*;

import java.util.List;

public class DeckTest {
    @Test
    public void shuffleTest(){
        Deck deck = new Deck();
        List<Card> original = deck.getCards();
        deck.Shuffle();
        List<Card> shuffled = deck.getCards();

        assertThat(shuffled, IsIterableContainingInAnyOrder.containsInAnyOrder(original.toArray()));
    }
}

/**
 * Created by omerh on 4/8/2017.
 */
public class PlayerHand {
    private Card first;
    private Card second;

    public PlayerHand(Card first, Card second) {
        this.first = first;
        this.second = second;
    }

    public Card getFirst() {
        return first;
    }

    public Card getSecond() {
        return second;
    }
}

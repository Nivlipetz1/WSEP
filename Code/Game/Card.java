/**
 * Created by omerh on 4/8/2017.
 */


public class Card {
    public enum Suit { CLUB, DIAMOND, SPADE, HEART }
    private Suit suit;
    private int value;

    public Card(int value, Suit suit){
        this.value = value;
        this.suit = suit;
    }

    public Suit getSuit() {
        return suit;
    }

    public int getValue() {
        return value;
    }

    @Override
    public String toString() {
        return "Card{" +
                "suit=" + suit +
                ", value=" + value +
                '}';
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;

        Card card = (Card) o;

        if (value != card.value) {
                return false;
        }
        return suit == card.suit;
    }

    @Override
    public int hashCode() {
        int result = suit != null ? suit.hashCode() : 0;
        result = 31 * result + value;
        return result;
    }
}

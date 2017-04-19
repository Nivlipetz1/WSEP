using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaming
{
    public class CardAnalyzerTests
    {

     public static void randomDrawTest(){
        Card[] cardsOnTable = new Card[5];
        Deck deck = new Deck();
            deck.Shuffle();
        PlayerHand hand1 = deck.drawPlayerHand();
        PlayerHand hand2 = deck.drawPlayerHand();
        Card[] flop = deck.drawFlop();

        for(int i=0;i<3;i++) {
            cardsOnTable[i] = flop[i];
        }
        cardsOnTable[3] = deck.drawTurn();
        cardsOnTable[4] = deck.drawRiver();
        CardAnalyzer analyzer = new CardAnalyzer(); /*niv changed this*/
        analyzer.setCardArray(cardsOnTable); /*niv changed this*/
        analyzer.setHand(hand1);
        CardAnalyzer.HandRank hand1Rank = analyzer.analyze();
        Console.WriteLine("Hand 1 has: " + hand1Rank + " Numeral Rank: "+(int)hand1Rank);
        analyzer.setHand(hand2);
        CardAnalyzer.HandRank hand2Rank = analyzer.analyze();
        Console.WriteLine("Hand 2 has: " + hand2Rank + " Numeral Rank: "+(int)hand2Rank);
        //Console.WriteLine("Comparison " + hand1Rank.compareTo(hand2Rank));
        if (hand1Rank.CompareTo(hand2Rank)<0){
            Console.WriteLine("Hand 1 is the winner!");
        }
        else if(hand1Rank.CompareTo(hand2Rank)>0) {
            Console.WriteLine("Hand 2 is the winner!");
        }
        else{
            Console.WriteLine("Tie Breaker");
            PlayerHand winner = analyzer.tieBreaker(hand1Rank,hand1, hand2);
            if(winner==null){
                Console.WriteLine("its a tie");
            }
            else if(winner.Equals(hand1)){
                Console.WriteLine("Hand 1 is the winner!");
            }
            else
                Console.WriteLine("Hand 2 is the winner!");
        }

        Console.WriteLine("On The Table: ");
        for(int i=0;i<5;i++){
            Console.WriteLine(cardsOnTable[i].toString());
        }
        Console.WriteLine();

        Console.WriteLine("Hand 1: "+hand1.toString());
        Console.WriteLine("Hand 2: "+hand2.toString());

    }
    }

}
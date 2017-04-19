using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User;

namespace Gaming
{
    public class Move
    {
        private IDictionary<PlayingUser, PlayerHand> gameHands;
        private int[] betMove;
        private Card[] revealedCards;
        private MoveType type;

        public enum MoveType
        {
            Bet,
            NewCard,
            RevealHand,
            GameStarted,
            GameEnded
        }

        public Move(MoveType type, IDictionary<PlayingUser, PlayerHand> gameHands)
        {
            this.gameHands = gameHands;
            this.type = type;
        }
        public Move(MoveType type, IDictionary<PlayingUser, PlayerHand> gameHands, int[] betMove) : this(type,gameHands)
        {
            this.betMove = betMove;
        }
        public Move(MoveType type, IDictionary<PlayingUser, PlayerHand> gameHands, Card[] revealedCards) : this(type, gameHands)
        {
            this.revealedCards = revealedCards;
        }
    }
}
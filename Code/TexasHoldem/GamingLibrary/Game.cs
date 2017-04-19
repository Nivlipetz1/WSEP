using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User;

namespace Gaming
{
    public class Game
    {
        private Deck gameDeck; 
        private List<PlayingUser> players; //these will be of type playingUsers
        private List<SpectatingUser> spectators; //these will be of type spectators
        private int[] pot; // pot[0] = main pot ||| pot[1] = sidepot
        private int bettingRound;
        private CardAnalyzer ca;
        private int playerCounter = 0;
        private GamePreferences gamePref;
        private Boolean revealed = false;
        private int dealerOffSet = 0;

        public Game(GamePreferences gp, PlayingUser creatingPlayer, int buyIn)
        {
            gameDeck = new Deck();
            players = new List<PlayingUser>();
            spectators = new List<SpectatingUser>();
            pot = new int[2];
            ca = new CardAnalyzer();
            players.Add(creatingPlayer);
            gamePref = gp;            
        }

        public void StartGame()
        {
            int potInt = pot[0];
            //announce game started

            //choose dealer
            dealerOffSet = 0;

            //request small blind
            PlayingUser smallBlindPlayer = players.ElementAt((dealerOffSet + 1) % players.Count);
            bettingRound += smallBlindPlayer.Bet(gamePref.GetsB());

            //send small blind move



            //request big blind
            PlayingUser bigBlindPlayer = players.ElementAt((dealerOffSet+2)%players.Count);
            bettingRound += smallBlindPlayer.Bet(gamePref.GetbB());

            //send big blind move

            //request bet from rest of players





            //end of betting round:

            pot[0] += bettingRound;

        }

        public GamePreferences GetGamePref()
        {
            return gamePref;
        }

        internal void Message(SpectatingUser spectaitngUser, string v)
        {
            throw new NotImplementedException();
        }

        private void addPlayer(UserProfile player)
        {
            if (players.Count == gamePref.GetMax())
                throw new InvalidOperationException("Maximum number of players reached");

            PlayingUser newPlayer = new PlayingUser(player, 0, this);
            players.Add(newPlayer);
        }

        private void removePlayer(UserProfile player)
        {
            if (players.Count == 0)
                throw new InvalidOperationException("No players to remove");

            PlayingUser removeThisPlayer = null;

            foreach(PlayingUser pl in players){
                if (pl.GetAccount() == player)
                    removeThisPlayer = pl;
            }

            if (removeThisPlayer == null)
                throw new InvalidOperationException("player.getName()" + " is not playing in this game");

            players.Remove(removeThisPlayer);
        }

        private void addSpectator(UserProfile user)
        {
            if (!gamePref.AllowSpec())
                throw new InvalidOperationException("Spectating is not allowed in this game");

            SpectatingUser spec = new SpectatingUser(user, this);

            spectators.Add(spec);
        }

        private void removeSpectator(UserProfile spec)
        {
            if (spectators.Count == 0)
                throw new InvalidOperationException("No spectators to remove");

            SpectatingUser removeThisPlayer = null;

            foreach (SpectatingUser pl in spectators)
            {
                if (pl.GetAccount() == spec)
                    removeThisPlayer = pl;
            }

            if (removeThisPlayer == null)
                throw new InvalidOperationException("player.getName()" + " is not spectating in this game");

            spectators.Remove(removeThisPlayer);
        
        }


        /**
         * 
         * Method where the player will choose to bet/raise/fold/check
         */

        public void turn()
        {
            PlayingUser nextP = nextPlayer();

            //does this need to be asynchronous / lambda function? we are waiting for a callback (bet amount) from the player to bet
            int betAmount = nextP.Bet(gamePref.GetbB());//need to send minimum bet to player
            bettingRound += betAmount;

            if (true)
            {
                pot[CheckPot()] = bettingRound;
                bettingRound = 0;
            }
        }


        /** 
         * This method will return only the players with active hands in the game (not the ones who folded/went all in)
         * 
         * **/
        private PlayingUser nextPlayer()
        {
            playerCounter++;
            PlayingUser nextPlayer = players.ElementAt(playerCounter % players.Count); 
            while(nextPlayer.GetStatus()!="Active"){
                playerCounter++;
                nextPlayer = players.ElementAt(playerCounter % players.Count); 
            }

            return nextPlayer;
        }


        /**
         * 
         * Check if there is a sidepot
         * 
         **/
        private int CheckPot()
        {
            if (!CheckSidePot())
                return 0;
            else
                return 1; 
        }

        private Boolean CheckSidePot() 
        {
            foreach (PlayingUser pl in players)
            {
                if (pl.GetStatus() == "AllIn")
                {
                    return true;
                }
            }
            return false;
        }

        private IDictionary<PlayingUser, PlayerHand> getPlayerDictionary(bool revealedFlag)
        {
            IDictionary<PlayingUser, PlayerHand> playersAndHands = new Dictionary<PlayingUser, PlayerHand>();
            foreach (PlayingUser player in players)
            {
                PlayerHand hand = null;
                if (revealedFlag)//Reveal only if needs to
                    hand = player.getHand();
            }

            return playersAndHands;
        }

        private Move makeBetMove(bool revealedFlag, int[] betMove)
        {
            return new Move(Move.MoveType.Bet, getPlayerDictionary(revealedFlag), betMove);
        }

        private Move makeCardRevealMove(bool revealedFlag, Card[] revealedCards)
        {
            return new Move(Move.MoveType.Bet, getPlayerDictionary(revealedFlag),revealedCards);
        }

        private void PushMovePlayers(Move move)
        {
            foreach (SpectatingUser player in players)
                player.PushMove(move);
        }

        private void PushMoveSpectators(Move move)
        {
            foreach (SpectatingUser spectator in spectators)
                spectator.PushMove(move);
        }

    }
}

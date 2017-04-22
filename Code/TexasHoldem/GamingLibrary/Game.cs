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
        private GameLogger logger;
        private IDictionary<string, int> playerBets = new Dictionary<string,int>();
        private Card[] cards;

        public Game(GamePreferences gp, PlayingUser creatingPlayer, int buyIn)
        {
            gameDeck = new Deck();
            players = new List<PlayingUser>();
            spectators = new List<SpectatingUser>();
            pot = new int[2];
            ca = new CardAnalyzer();
            players.Add(creatingPlayer);
            playerBets.Add(creatingPlayer.GetAccount().Username, 0);
            gamePref = gp;
            logger = new GameLogger();
        }

        public void StartGame()
        {
            int potInt = pot[0];
            //announce game started

            //send the array of players to all the observers
            PushMoveToObservers(new GameStartMove(playerBets));

            //choose dealer
            dealerOffSet = 0;

            //request small blind
            PlayingUser smallBlindPlayer = players.ElementAt((dealerOffSet + 1) % players.Count);
            int smallBlindBet = smallBlindPlayer.Bet(gamePref.GetsB());
            bettingRound += smallBlindBet;
            playerBets[smallBlindPlayer.GetAccount().Username] = smallBlindBet; 

            //send small blind move
            PushMoveToObservers(new BetMove(playerBets));

            //request big blind
            PlayingUser bigBlindPlayer = players.ElementAt((dealerOffSet+2)%players.Count);
            int bigBlindBet = bigBlindPlayer.Bet(gamePref.GetbB());
            bettingRound += bigBlindBet;
            playerBets[bigBlindPlayer.GetAccount().Username] = bigBlindBet; 

            //send big blind move
            PushMoveToObservers(new BetMove(playerBets));

            //draw hands
            foreach (PlayingUser player in players)
            {
                player.SetHand(gameDeck.drawPlayerHand());
            }


            //request bet from rest of players
            TraversePlayers(dealerOffSet + 3);

            cards = new Card[5]; 
            Card[] flop = gameDeck.drawFlop();
            cards[0] = flop[0];
            cards[1] = flop[1];
            cards[2] = flop[2];

            PushMoveToObservers(new NewCardMove(cards));

            TraversePlayers(dealerOffSet + 1);

            cards[3] = gameDeck.drawRiver();

            PushMoveToObservers(new NewCardMove(cards));

            TraversePlayers(dealerOffSet + 1);

            cards[4] = gameDeck.drawTurn();

            PushMoveToObservers(new NewCardMove(cards));

            TraversePlayers(dealerOffSet + 1);


        }

        private void DetermineWinner()
        {
            Dictionary<PlayingUser,CardAnalyzer.HandRank> playerScores= new Dictionary<PlayingUser,CardAnalyzer.HandRank>();
            ca.setCardArray(cards);
            foreach (PlayingUser player in players)
            {
                if (player.GetStatus() != "Fold")
                {
                    ca.setHand(player.GetHand());
                    playerScores.Add(player,ca.analyze());
                }
            }
            CardAnalyzer.HandRank minValue = playerScores.Values.Min();
            //NEED TO GROUP BY MINVALUE AND DO A TOURNAMENT TEST TO DETERMINE THE WINNER USING THE
            //TIEBREAKER FUNCTION IN CARDANALYZER
            foreach (PlayingUser player in playerScores.Keys)
            {
                if (playerScores[player] != minValue)
                    playerScores.Remove(player);
            }
            
//            ca.tieBreaker()
        }

        private int GetMaxBet()
        {
            return playerBets.Values.Max();
        }

        private void TraversePlayers(int index){
            while (!EndOfBettingRound())
            {

                string currentUser = players.ElementAt(index).GetAccount().Username;
                int bet = players[index].Bet(GetMaxBet()-playerBets[currentUser]);
                playerBets[currentUser] += bet;
                bettingRound += bet;
                PushMoveToObservers(new BetMove(playerBets));
                index = index+1 % players.Count;
            }
            pot[0] += bettingRound;
            bettingRound = 0;

            foreach (PlayingUser player in players)
            {
                if (player.GetStatus() == "Talked")
                {
                    player.SetStatus("Active");
                }
            }
        }

        private bool EndOfBettingRound()
        {
            List<int> exceptThis = new List<int>();
            exceptThis.Add(-1); //take out the folds

            foreach(PlayingUser player in players){
                if (player.GetStatus() == "Active"){
                    return false;
                }
            }

            return playerBets.Values.ToList().Except(exceptThis).Distinct().ToList().Count == 1;
           
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
            playerBets.Add(newPlayer.GetAccount().Username, 0);

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
            playerBets.Remove(removeThisPlayer.GetAccount().Username);
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
                    hand = player.GetHand();
            }

            return playersAndHands;
        }

        /*private Move makeBetMove(bool revealedFlag, int[] betMove)
        {
            return new Move(Move.MoveType.Bet, getPlayerDictionary(revealedFlag), betMove);
        }

        private Move makeCardRevealMove(bool revealedFlag, Card[] revealedCards)
        {
            //return new Move(Move.MoveType.Bet, getPlayerDictionary(revealedFlag),revealedCards);
        }*/

        private void PushMoveToObservers(Move m){
            foreach (SpectatingUser spectator in spectators)
                spectator.PushMove(m);
            foreach (PlayingUser player in players)
                player.PushMove(m);
            logger.AddMove(m);
        }

    }
}

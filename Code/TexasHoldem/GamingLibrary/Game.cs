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
        private IDictionary<PlayingUser, int> playerBets = new Dictionary<PlayingUser,int>();
        private Card[] cards;

        public Game(GamePreferences gp, int buyIn)
        {
            gameDeck = new Deck();
            players = new List<PlayingUser>();
            spectators = new List<SpectatingUser>();
            pot = new int[2];
            ca = new CardAnalyzer();
            gamePref = gp;
            logger = new GameLogger();
        }

        public void StartGame()
        {
            int potInt = pot[0];
            //announce game started

            //send the array of players to all the observers
            PushStartGameMove();

            //choose dealer
            dealerOffSet = 0;

            //request small blind
            PlayingUser smallBlindPlayer = players.ElementAt((dealerOffSet + 1) % players.Count);
            int smallBlindBet = smallBlindPlayer.Bet(gamePref.GetsB());
            bettingRound += smallBlindBet;
            playerBets[smallBlindPlayer] = smallBlindBet;

            //send small blind move
            PushBetMove();

            //request big blind
            PlayingUser bigBlindPlayer = players.ElementAt((dealerOffSet+2)%players.Count);
            int bigBlindBet = bigBlindPlayer.Bet(gamePref.GetbB());
            bettingRound += bigBlindBet;
            playerBets[bigBlindPlayer] = bigBlindBet;

            //send big blind move
            PushBetMove();

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

        private List<PlayingUser> DetermineWinner()
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
            List<PlayingUser> winners = new List<PlayingUser>();
            foreach (PlayingUser player in playerScores.Keys)
            {
                if (winners.Count == 0)
                {
                    winners.Add(player);
                }
                else
                {
                    PlayerHand winner = ca.tieBreaker(minValue,winners.First().GetHand(),player.GetHand());
                    if (winner == null)
                    {
                        winners.Add(player);
                    }
                    else if(winner == player.GetHand())
                    {
                        winners.Clear();
                        winners.Add(player);
                    }
                }
            }
            return winners;
        }

        private int GetMaxBet()
        {
            return playerBets.Values.Max();
        }

        private void PushBetMove()
        {
            IDictionary<string, int> playerBetsString = new Dictionary<string, int>();
            foreach(PlayingUser player in playerBets.Keys)
            {
                playerBetsString.Add(player.GetAccount().Username, playerBets[player]);
            }
            PushMoveToObservers(new BetMove(playerBetsString));
        }

        private void PushStartGameMove()
        {
            IDictionary<string, int> playerBetsString = new Dictionary<string, int>();
            foreach (PlayingUser player in playerBets.Keys)
            {
                playerBetsString.Add(player.GetAccount().Username, playerBets[player]);
            }
            PushMoveToObservers(new GameStartMove(playerBetsString));
        }

        private void TraversePlayers(int index){
            while (!EndOfBettingRound())
            {

                PlayingUser currentUser = players.ElementAt(index);
                int bet = players[index].Bet(GetMaxBet()-playerBets[currentUser]);
                playerBets[currentUser] += bet;
                bettingRound += bet;
                PushBetMove();
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
            playerBets.Add(newPlayer, 0);

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
            playerBets.Remove(removeThisPlayer);
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

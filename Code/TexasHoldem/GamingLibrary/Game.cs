using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameUtilities;

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
        private GamePreferences gamePref;
        private GameLogger logger;
        private IDictionary<PlayingUser, int> playerBets = new Dictionary<PlayingUser, int>();
        private IDictionary<string, PlayerHand> playerHands = new Dictionary<string, PlayerHand>();
        private Card[] cards;
        private GameChat chat;
        private bool gameEnded;

        public delegate void Update(UserProfile user);
        public event Update evt;

        public Game(GamePreferences gp)
        {
            gameDeck = new Deck();
            players = new List<PlayingUser>();
            spectators = new List<SpectatingUser>();
            pot = new int[2];
            ca = new CardAnalyzer();
            gamePref = gp;
            logger = new GameLogger();
            chat = new GameChat(this);
        }

        public GameChat GetChat()
        {
            return chat;
        }

        public void StartGame()
        {

            if (gamePref.GetMinPlayers() > GetNumberOfPlayers())
                throw new InvalidOperationException("Can't start game with less than the minimum number of players");

            int potInt = pot[0];
            //announce game started
            gameEnded = false;
            gameDeck.Shuffle();

            //send the array of players to all the observers
            PushStartGameMove();


            //request small blind
            PlayingUser smallBlindPlayer = players.ElementAt(0);
            int smallBlindBet = smallBlindPlayer.GetBlind(gamePref.GetsB());
            bettingRound += smallBlindBet;
            playerBets[smallBlindPlayer] = smallBlindBet;

            //send small blind move
            PushBetMove();

            //request big blind
            PlayingUser bigBlindPlayer = players.ElementAt(1);
            int bigBlindBet = bigBlindPlayer.GetBlind(gamePref.GetbB());
            bettingRound += bigBlindBet;
            playerBets[bigBlindPlayer] = bigBlindBet;

            //send big blind move
            PushBetMove();

            //draw hands
            foreach (PlayingUser player in players)
            {
                PlayerHand ph = gameDeck.drawPlayerHand();
                player.SetHand(ph);
                playerHands.Add(player.GetAccount().Username, ph);
            }


            //request bet from rest of players

            TraversePlayers(2 % players.Count);
            if (gameEnded)
            {
                GiveWinnings();
                PushMoveToObservers(new EndGameMove(playerHands));
                ResetGame();
                return;
            }

            cards = new Card[5];
            Card[] flop = gameDeck.drawFlop();
            cards[0] = flop[0];
            cards[1] = flop[1];
            cards[2] = flop[2];

            PushMoveToObservers(new NewCardMove(cards));

            TraversePlayers(0);
            if (gameEnded)
            {
                GiveWinnings();
                PushMoveToObservers(new EndGameMove(playerHands));
                ResetGame();
                return;
            }

            cards[3] = gameDeck.DrawTableCard();

            PushMoveToObservers(new NewCardMove(cards));

            TraversePlayers(0);
            if (gameEnded)
            {
                GiveWinnings();
                PushMoveToObservers(new EndGameMove(playerHands));
                ResetGame();
                return;
            }

            cards[4] = gameDeck.DrawTableCard();

            PushMoveToObservers(new NewCardMove(cards));

            TraversePlayers(0);
            if (gameEnded)
            {
                GiveWinnings();
                PushMoveToObservers(new EndGameMove(playerHands));
                ResetGame();
                return;
            }

            PushMoveToObservers(new EndGameMove(playerHands));
            List<PlayingUser> winners = DetermineWinner();
            foreach (PlayingUser player in winners)
            {
                player.ReceiveWinnings(pot[0] / winners.Count);
            }

            ResetGame();

        }
        private void GiveWinnings()
        {
            foreach (PlayingUser player in players)
            {
                if (player.GetStatus() != "Fold")
                    player.ReceiveWinnings(pot[0]);
            }
        }


        private void ResetGame()
        {
            pot[0] = 0;
            bettingRound = 0;
            cards = null;
            gameDeck = new Deck();
            PlayingUser dealer = players.First();
            players.Remove(dealer);
            players.Insert(players.Count, dealer);

            foreach (PlayingUser player in players)
            {
                player.SetStatus("Active");
                playerHands.Remove(player.GetAccount().Username);
                playerBets[player] = 0;
            }

        }

        private List<PlayingUser> DetermineWinner()
        {
            Dictionary<PlayingUser, CardAnalyzer.HandRank> playerScores = new Dictionary<PlayingUser, CardAnalyzer.HandRank>();
            ca.setCardArray(cards);
            foreach (PlayingUser player in players)
            {
                if (player.GetStatus() != "Fold")
                {
                    ca.setHand(player.GetHand());
                    playerScores.Add(player, ca.analyze());
                }
            }

            CardAnalyzer.HandRank minValue = CardAnalyzer.HandRank.HighCard;
            foreach (CardAnalyzer.HandRank rank in playerScores.Values)
            {
                if (rank < minValue)
                    minValue = rank;
            }

            Dictionary<PlayingUser, CardAnalyzer.HandRank> bestPlayerScores = new Dictionary<PlayingUser, CardAnalyzer.HandRank>();

            foreach (PlayingUser player in playerScores.Keys)
            {
                if (playerScores[player] == minValue)
                    bestPlayerScores.Add(player, playerScores[player]);
            }



            List<PlayingUser> winners = new List<PlayingUser>();
            foreach (PlayingUser player in bestPlayerScores.Keys)
            {
                if (winners.Count == 0)
                {
                    winners.Add(player);
                }
                else
                {
                    PlayerHand winner = ca.tieBreaker(minValue, winners.First().GetHand(), player.GetHand());
                    if (winner == null)
                    {
                        winners.Add(player);
                    }
                    else if (winner == player.GetHand())
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
            foreach (PlayingUser player in playerBets.Keys)
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

        private void TraversePlayers(int index)
        {
            while (!EndOfBettingRound())
            {
                Console.WriteLine(index);
                PlayingUser currentUser = players.ElementAt(index);
                int minimumBet = GetMaxBet() - playerBets[currentUser];
                if (currentUser.GetStatus() != "Fold")
                {
                    int bet = players[index].Bet(minimumBet);
                    while (bet < minimumBet && bet >= 0)
                    {
                        bet = currentUser.BadBet(bet, minimumBet);
                    }
                    if (bet >= 0) //check|call|raise
                    {
                        playerBets[currentUser] += bet;
                        bettingRound += bet;
                    }
                    else //fold
                    {
                        playerBets[currentUser] = bet;
                        playerHands.Remove(currentUser.GetAccount().Username);
                    }
                    PushBetMove();
                    if (DidEveryoneFold())
                    {
                        pot[0] += bettingRound;
                        bettingRound = 0;
                        gameEnded = true;
                        return;
                    }
                }
                index++;
                if (index == players.Count)
                    index = 0;
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


        private bool DidEveryoneFold()
        {
            int inc = 0;
            foreach (PlayingUser player in playerBets.Keys)
            {
                if (playerBets[player] != -1)
                    inc++;
            }
            return (inc == 1);
        }

        private bool EndOfBettingRound()
        {
            foreach (PlayingUser player in players)
            {
                if (player.GetStatus() == "Active")
                {
                    return false;
                }
            }

            List<int> checkDistinctAmounts = playerBets.Values.ToList().Except(new List<int> { -1 }).Distinct().ToList();
            return (checkDistinctAmounts.Count == 1);
        }


        internal void Message(SpectatingUser spectaitngUser, string v)
        {
            throw new NotImplementedException();
        }

        public void addPlayer(PlayingUser player)
        {
            if (players.Count == gamePref.GetMaxPlayers())
                throw new InvalidOperationException("Maximum number of players reached");

            player.GetAccount().Credit -= player.GetCredit();
            players.Add(player);
            playerBets.Add(player, 0);
        }

        public void removePlayer(PlayingUser player)
        {
            if (players.Count == 0)
                throw new InvalidOperationException("No players to remove");

            if (!players.Contains(player))
                throw new InvalidOperationException("Player not in game");

            player.GetAccount().Credit += player.GetCredit();
            players.Remove(player);
            playerBets.Remove(player);

            var e = evt;
            if (e != null)
                evt(player.GetAccount());

            player = null;

        }

        public void addSpectator(SpectatingUser spec)
        {
            if (!gamePref.AllowSpec())
                throw new InvalidOperationException("Spectating is not allowed in this game");

            spectators.Add(spec);
        }

        public void removeSpectator(SpectatingUser spec)
        {
            if (spectators.Count == 0)
                throw new InvalidOperationException("No spectators to remove");

            if (!spectators.Contains(spec))
                throw new InvalidOperationException("player.getName()" + " is not spectating in this game");

            spectators.Remove(spec);
            spec = null;
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

        private void PushMoveToObservers(Move m)
        {
            foreach (SpectatingUser spectator in spectators)
                spectator.PushMove(m);
            foreach (PlayingUser player in players)
                player.PushMove(m);
            logger.AddMove(m);
        }

        public GameLogger GetLogger()
        {
            return logger;
        }

        public List<PlayingUser> GetPlayers()
        {
            return players;
        }

        public List<SpectatingUser> GetSpectators()
        {
            return spectators;
        }

        public Card[] GetCards()
        {
            return cards;
        }

        public GamePreferences GetGamePref()
        {
            return gamePref;
        }

        public int GetPotSize()
        {
            return pot[0];
        }

        public List<UserProfile> GetUserProfiles()
        {
            List<UserProfile> users = new List<UserProfile>();
            foreach (PlayingUser player in players)
            {
                users.Add(player.GetAccount());
            }
            return users;
        }

        public int GetNumberOfPlayers()
        {
            return players.Count;
        }

        public List<Move> GetGameReplay()
        {
            return logger.GetMoves();
        }

        public void InactivateGame()
        {
            gamePref.SetStatus("inactive");
        }
    }
}
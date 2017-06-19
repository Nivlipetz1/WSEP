using System;
using System.Collections.Generic;
using System.Linq;
using Gaming;
using System.Threading;
using GameSystem.Data_Layer;

namespace GameSystem
{
    public class GameCenter : LeagueAPI , GameCenterInterface
    {
        List<Game> games = new List<Game>();
        Dictionary<int, League> leagues = new Dictionary<int, League>();
        private ICollection<UserProfile> Users;

        public class GameCenterFactory
        {
            private static GameCenter instance = null;

            public static GameCenter getInstance()
            {
                if (instance == null)
                    return instance=new GameCenter();
                return instance;
            }
            public static void clean()
            {
                instance = null;
            }
        }

        private GameCenter()
        {
            Users = new List<UserProfile>();
            List<League> l = DBConnection.Instance.getLeagues();
            foreach(League league in l)
            {
                leagues.Add(league.minimumRank, league);
            }
            if(!leagues.ContainsKey(0))
            {
                League league = new League(0, "deafult league");
                league.Save();
                leagues.Add(0, league);
            }
        }

        public Game createGame(GamePreferences preferecnces , UserProfile user)
        {
            int gameId = DBConnection.Instance.getNewGameId();
            Game game = new Game(gameId, preferecnces);
            games.Add(game);
            user.League.addGame(game);
            game.evt += updateLeagueToUser;
            return game;
        }

        public Game getGameByID(int gameID)
        {
            List<Game> gamesID = games.Where(game => game.getGameID() == gameID).ToList();
            return gamesID.Count == 1 ? gamesID[0] : null;
        }

        delegate List<Game> activeGame(object param , List<Game> games);
        public List<Game> getActiveGames(String criterion , object param , UserProfile user)
        {
            activeGame func;
            List<Game> gamesInLeague = games.Where(game => user.League.getGames().Contains(game)).ToList();
            gamesInLeague = gamesInLeague.Where(game => game.GetGamePref().GetStatus().Equals("Active") || game.GetGamePref().GetStatus().Equals("Init")).ToList();
            switch (criterion)
            {
                case "playerName":
                    func = getAllActiveGamesByPlayerName;
                    break;
                case "potsize":
                    func = getAllActiveGamesByPotSize;
                    break;
                case "gamepreference":
                    func = getAllActiveGamesByGamePreference;
                    break;

                default:
                    return gamesInLeague;
            }

            return func(param, gamesInLeague);
        }

        public List<Game> getAllSpectatingGames()
        {
            return games.Where(game => game.GetGamePref().AllowSpec()).ToList();
        }

        private List<Game> getAllActiveGamesByPlayerName(object playerName , List<Game> games)
        {
            List<Game> activeGames = new List<Game>();
            foreach (Game game in games)
            {
                List<PlayingUser> players = game.GetPlayers();

                if (players.Where(u => ((PlayingUser)u).GetUserName().Equals(playerName)).ToList().Count > 0)
                    activeGames.Add(game);
            }

            return activeGames;
        }

        private List<Game> getAllActiveGamesByPotSize(object potSize , List<Game> games)
        {
            return games.Where(game => game.GetPotSize() == (int)potSize).ToList();
        }

        private List<Game> getAllActiveGamesByGamePreference(object preferences , List<Game> games)
        {
            return games.Where(game => contained(game.GetGamePref(), (GamePreferences)preferences)).ToList();
        }

        private bool contained(GamePreferences gamePreferences, GamePreferences preferences)
        {
            int smallBlind = gamePreferences.GetsB();
            int bigBlind = gamePreferences.GetbB();
            int typePolicy = gamePreferences.GetTypePolicy();
            int buyInPolicy = gamePreferences.GetBuyInPolicy();
            int chipPolicy = gamePreferences.GetChipPolicy();

            if (preferences.GetsB() > 0 && preferences.GetsB() != smallBlind)
                return false;

            if (preferences.GetbB() > 0 && preferences.GetbB() != bigBlind)
                return false;

            if (preferences.GetTypePolicy() > 0 && preferences.GetTypePolicy() != typePolicy)
                return false;

            if (preferences.GetBuyInPolicy() > 0 && preferences.GetBuyInPolicy() != buyInPolicy)
                return false;

            if (preferences.GetChipPolicy() > 0 && preferences.GetChipPolicy() != chipPolicy)
                return false;


            return true;
        }

        public bool joinGame(Game game, UserProfile u, int credit)
        {
            if (credit > u.Credit)
                return false;
            if (game.GetGamePref().GetBuyInPolicy() > credit)
                return false;
            if (game.GetNumberOfPlayers() == game.GetGamePref().GetMaxPlayers())
                return false;

            PlayingUser playingUser = new PlayingUser(u.Username, credit - game.GetGamePref().BuyInPolicy, game);
            game.addPlayer(playingUser);

            u.UpdateCredit(-credit);
            return true;
        }

        public bool spectateGame(Game game, UserProfile u)
        {
            if (!game.GetGamePref().AllowSpec())
                return false;

            SpectatingUser spectatingUser = new SpectatingUser(u.Username, game);
            game.addSpectator(spectatingUser);

            return true;
        }

        public List<List<Move>> getAllReplayesOfInActiveGames()
        {
            List<List<Move>> replayes = new List<List<Move>>();
            games.Where(game => !game.GetGamePref().GetStatus().Equals("Active")).ToList().ForEach(game => replayes.Add(game.GetGameReplay()));

            return replayes;

        }
        public List<Move> getReplayByGameId(int gameId)
        {
            return DBConnection.Instance.getReplayById(gameId).gameMoves;
        }

        public List<int> getAllAvailableReplayes()
        {
            return DBConnection.Instance.getAllAvailableReplayes();
        }

        public bool createNewLeague(int minimumRank)
        {
            foreach (League l in leagues.Values)
            {
                if (l.MinimumRank <= minimumRank && minimumRank < l.MinimumRank + 1000)
                    return false;
            }
            League league = new League(minimumRank, "League" + leagues.Count);
            leagues.Add(minimumRank, league);
            league.Save();
            return true;
        }

        public bool removeUserFromLeague(UserProfile user, League league)
        {
            return league.removeUser(user);
        }

        public League getLeagueByUser(UserProfile user)
        {
            foreach (League league in leagues.Values)
            {
                if (league.isUser(user))
                    return league;
            }
            return null;
        }
        public League getLeagueByRank(int Rank)
        {
            try {
                return leagues[Rank];
            }
            catch
            {
                return null;
            }
        }

        public void updateLeagueToUser(PlayingUser playingUser)
        {
            UserProfile user = GetUserByName(playingUser.GetUserName());
            user.UpdateCredit(playingUser.GetCredit());
            user.updateStatistics(playingUser);

            if (user.UserStat.Winnings + user.UserStat.Losses < 10)
                return;
            League currLeague = user.League;
            foreach (League league in leagues.Values)
            {
                if (league.MinimumRank <= user.Credit && league.MinimumRank + 1000 > user.Credit)
                {
                    currLeague.removeUser(user);
                    league.addUser(user);
                    user.League = league;
                    goto End;
                }
            }

            createNewLeague((user.Credit / 1000) * 1000);
            currLeague.removeUser(user);
            League l = getLeagueByRank((user.Credit / 1000) * 1000);
            l.addUser(user);
            user.League = l;
        End:
            redistributesLeagues();
        }


        public void setUsers(ICollection<UserProfile> Users)
        {
            this.Users = Users;
        }


        public Dictionary<int,League> getLeagues()
        {
            return leagues;
        }
        
        public UserProfile GetUserByName(string username){
            return TexasHoldemSystem.userSystemFactory.getInstance().getUser(username);
        }

        public bool unknownUserEditLeague(UserProfile user, League league)
        {
            if (user.UserStat.Losses + user.UserStat.Winnings >= 10)
                return false;
            league.addUser(user);
            user.League = league;
            return true;
        }

        public League getLeagueByID(int id)
        {
            if(leagues.ContainsKey(id))
                return leagues[id];
            return null;
        }

        private void redistributesLeagues()
        {
            DBConnection dbcon = DBConnection.Instance;
            int numberOfUsers = dbcon.GetUsers().Count;
            int numberOfLeagues = dbcon.getLeagues().Count;
            double ratioPlayersPerLeague = numberOfUsers / numberOfLeagues;
            int numberOfLeagues_should = numberOfLeagues;

            if (ratioPlayersPerLeague < 2)
                numberOfLeagues_should = numberOfUsers / 2;
            ratioPlayersPerLeague = numberOfUsers / numberOfLeagues_should;

            List<League> leagues = dbcon.getLeagues();
            for(int i = numberOfLeagues - 1 ; i >= numberOfLeagues - numberOfLeagues_should; i--) {
                if(ratioPlayersPerLeague - leagues[i].users.Count > 1)
                    redistributesSpecificLeague(leagues, i, true, (int)Math.Floor(ratioPlayersPerLeague-leagues[i].users.Count), numberOfLeagues_should);
                if(leagues[i].users.Count - ratioPlayersPerLeague > 1)
                    redistributesSpecificLeague(leagues, i, false, (int)Math.Floor(leagues[i].users.Count - ratioPlayersPerLeague), numberOfLeagues_should);
            }
        }
        
        private void redistributesSpecificLeague(List<League> leagues, int leagueIndex, bool isAdd, int numberOfPlayersToMove, int numberOfLeagues)
        {
            if (!isAdd)
            {
                List<UserProfile> leagueUsers = leagues[leagueIndex].users.OrderBy(user => user.Credit).ToList();
                leagues[leagueIndex - 1].addUsers(leagueUsers.GetRange(0, numberOfPlayersToMove));
                return;
            }

            for (int i = leagueIndex - 1; i >= leagues.Count - numberOfLeagues && numberOfPlayersToMove > 0; i--)
            {
                List<UserProfile> leagueUsers = leagues[i].users.OrderBy(user => user.Credit).ToList();
                int numberOfUserInPreviousLeague = leagueUsers.Count;
                numberOfUserInPreviousLeague = Math.Min(numberOfUserInPreviousLeague, numberOfPlayersToMove);
                leagues[leagueIndex].addUsers(leagueUsers.GetRange(leagueUsers.Count - 1 - numberOfUserInPreviousLeague, numberOfUserInPreviousLeague));
                numberOfPlayersToMove -= numberOfUserInPreviousLeague;
            }
        }
    }
}

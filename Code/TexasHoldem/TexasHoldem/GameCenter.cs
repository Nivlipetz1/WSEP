using System;
using System.Collections.Generic;
using System.Linq;
using Gaming;

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
        }

        private GameCenter()
        {
            Users = new List<UserProfile>();
        }

        public Game createGame(GamePreferences preferecnces , UserProfile user)
        {
            Game game = new Game(preferecnces);
            games.Add(game);
            user.League.getGames().Add(game);
            game.evt += updateLeagueToUser;
            return game;
        }

        delegate List<Game> activeGame(object param , List<Game> games);
        public List<Game> getActiveGames(String criterion , object param , UserProfile user)
        {
            activeGame func;
            List<Game> gamesInLeague = games.Where(game => user.League.getGames().Contains(game)).ToList();
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
            foreach (Game game in games.Where(game => game.GetGamePref().GetStatus().Equals("active")).ToList())
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

            PlayingUser playingUser = new PlayingUser(u.Username, credit, game);
            game.addPlayer(playingUser);

            u.Credit = u.Credit - credit;
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
            games.Where(game => !game.GetGamePref().GetStatus().Equals("active")).ToList().ForEach(game => replayes.Add(game.GetGameReplay()));

            return replayes;

        }

        public bool createNewLeague(int minimumRank)
        {
            foreach (League l in leagues.Values)
            {
                if (l.MinimumRank == minimumRank)
                    return false;
            }
            League league = new League(minimumRank, "League" + leagues.Count);
            leagues.Add(minimumRank, league);
            return true;
        }

        public bool addUserToLeague(UserProfile user, League league)
        {
            if (user.Credit < league.MinimumRank)
                return false;
            foreach(League lea in leagues.Values)
            {
                if (lea.removeUser(user))
                    break;
            }
            return league.addUser(user);
        }
        public bool removeUserFromLeague(UserProfile user, League league)
        {
            return league.removeUser(user);
        }
        public bool changeLeagueMinimumRank(League league, int newRank)
        {
            if (leagues.Keys.Contains(newRank))
                return false;
            league.update(newRank);
            return true;
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
                throw new InvalidOperationException("No league with Rank" + Rank);
            }
        }

        public void updateLeagueToUser(PlayingUser playingUser)
        {
            UserProfile user = GetUserByName(playingUser.GetUserName());
            user.Credit += playingUser.GetCredit();
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
                    return;
                }
            }

            createNewLeague((user.Credit / 1000) * 1000);
            currLeague.removeUser(user);
            League l = getLeagueByRank((user.Credit / 1000) * 1000);
            l.addUser(user);
            user.League = l;
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
            foreach (UserProfile user in Users)
            {
                if (user.Username == username)
                    return user;
            }

            return null;
        }




    }
}

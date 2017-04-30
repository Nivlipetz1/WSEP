﻿using System;
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
        private UserProfile HighestRankUser=null;



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
            leagues.Add(0, new League(0, "default"));
        }

        public Game createGame(GamePreferences preferecnces)
        {
            Game game = new Game(preferecnces);
            games.Add(game);

            game.evt += updateLeagueToUser;
            return game;
        }

        public List<Game> getAllSpectatingGames()
        {
            return games.Where(game => game.GetGamePref().AllowSpec()).ToList();
        }

        public List<Game> getAllActiveGamesByPlayerName(String playerName)
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

        public List<Game> getAllActiveGamesByPotSize(int potSize)
        {
            return games.Where(game => ((Game)game).GetPotSize() == potSize).ToList();
        }

        public List<Game> getAllActiveGamesByGamePreference(GamePreferences preferences)
        {
            return games.Where(game => contained(((Game)game).GetGamePref(), preferences)).ToList();
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
 
        public void updateState()
        {
            foreach (UserProfile user in Users)
            {
                if (HighestRankUser == null || HighestRankUser.Credit < user.Credit)
                    HighestRankUser = user;
            }
        }
        public void updateLeagueToUser(PlayingUser playingUser)
        {

            UserProfile user = GetUserByName(playingUser.GetUserName());
            user.Credit += playingUser.GetCredit();
            
            League currLeague = getLeagueByUser(user);
            if (HighestRankUser==null ||user.Credit > HighestRankUser.Credit)
                HighestRankUser = user;
            foreach (League league in leagues.Values)
            {
                if (league.MinimumRank <= user.Credit &&
                    (currLeague.MinimumRank > user.Credit || league.MinimumRank > currLeague.MinimumRank))
                {
                    currLeague.removeUser(user);
                    league.addUser(user);
                    currLeague = league;
                   // return;
                }
            }
        }
        public UserProfile getHighestRankUser()
        {
            return HighestRankUser;
        }

        public void setUsers(ICollection<UserProfile> Users)
        {
            this.Users = Users;
            updateState();
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

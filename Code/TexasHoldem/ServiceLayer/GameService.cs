using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameSystem;
using Gaming;
using ServiceLayer.Models;
using ServiceLayer.Interfaces;

namespace ServiceLayer
{
    public class GameService : GameServiceInterface
    {
        GameCenter gc = GameCenter.GameCenterFactory.getInstance();

        public List<string> bet(ClientUserProfile user, int gameID, string minimumBet)
        {
            Game game = gc.getGameByID(gameID);
            PlayingUser player = game.GetPlayers().Where(pu => pu.GetUserName().Equals(user.Username)).First();
            player.setInput(minimumBet);
            return game.GetPlayers().ConvertAll(x => (SpectatingUser)x).Union(game.GetSpectators()).Select(player1 => player1.GetUserName()).ToList();
        }

        public List<string> removePlayer(ClientUserProfile user, int gameID)
        {
            Game game = gc.getGameByID(gameID);
            PlayingUser player = game.GetPlayers().Where(pu => pu.GetUserName().Equals(user.Username)).First();
            game.removePlayer(player);
            return game.GetPlayers().ConvertAll(x => (SpectatingUser)x).Union(game.GetSpectators()).Select(player1 => player1.GetUserName()).ToList();
        }

        public List<string> removeSpectator(ClientUserProfile user, int gameID)
        {
            Game game = gc.getGameByID(gameID);
            SpectatingUser spec = game.GetSpectators().Where(sp => sp.GetUserName().Equals(user.Username)).First();
            game.removeSpectator(spec);
            return game.GetPlayers().ConvertAll(x => (SpectatingUser)x).Union(game.GetSpectators()).Select(player => player.GetUserName()).ToList();
        }

        public List<string> postMessage(ClientUserProfile user, string message, int gameID)
        {
            Game game = gc.getGameByID(gameID);
            SpectatingUser spec = game.GetSpectators().Where(sp => sp.GetUserName().Equals(user.Username)).First();
            List<SpectatingUser> specs = game.postMessage(spec, message);
            List<string> usernames = new List<string>();
            specs.ForEach(spec1 => usernames.Add(spec1.GetUserName()));
            return usernames;
        }

        public List<string> postWhisperMessage(ClientUserProfile from, ClientUserProfile to, string message, int gameID)
        {
            Game game = gc.getGameByID(gameID);
            SpectatingUser fromSpec = game.GetSpectators().Where(sp => sp.GetUserName().Equals(from.Username)).First();
            SpectatingUser toSpec = game.GetSpectators().Where(sp => sp.GetUserName().Equals(from.Username)).First();
            List<SpectatingUser> specs = game.postMessage(fromSpec, toSpec, message);
            List<string> usernames = new List<string>();
            specs.ForEach(spec => usernames.Add(spec.GetUserName()));
            return usernames;

        }

        private List<PlayingUser> GetPlayers(ClientUserProfile user, int gameID)
        {
            return gc.getGameByID(gameID).GetPlayers();
        }


















        /*
        public List<SpectatingUser> GetSpectators(UserProfile user, int gameID)
        {
            return gc.getGameByID(user, gameID).GetSpectators();
        }

        public GamePreferences GetGamePref(UserProfile user, int gameID)
        {
            return gc.getGameByID(user, gameID).GetGamePref();
        }

        public int GetPotSize(UserProfile user, int gameID)
        {
            return gc.getGameByID(user, gameID).GetPotSize();
        }

        public int GetNumberOfPlayers(UserProfile user, int gameID)
        {
            return gc.getGameByID(user, gameID).GetPlayers().Count;
        }

        public List<Move> GetGameReplay(UserProfile user, int gameID)
        {
            return gc.getGameByID(user, gameID).GetLogger().GetMoves();
        }
         * */
    }
}

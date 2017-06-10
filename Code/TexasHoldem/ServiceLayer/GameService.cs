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
        public static bool testable = true;
        GameCenter gc = GameCenter.GameCenterFactory.getInstance();

        public bool bet(string user, int gameID, string minimumBet)
        {
            Game game = gc.getGameByID(gameID);
            if (game == null)
                return false;
            PlayingUser player = game.getPlayerByUsername(user);
            return player.setInput(minimumBet);
        }

        public List<string> removePlayer(string user, int gameID)
        {
            Game game = gc.getGameByID(gameID);
            if (game == null)
                return null;
            PlayingUser player = game.getPlayerByUsername(user);
            game.removePlayer(player);
            return game.GetPlayers().ConvertAll(x => (SpectatingUser)x).Union(game.GetSpectators()).Select(player1 => player1.GetUserName()).ToList();
        }

        public List<string> removeSpectator(string user, int gameID)
        {
            Game game = gc.getGameByID(gameID);
            if (game == null)
                return null;
            SpectatingUser spec = game.GetSpectators().Where(sp => sp.GetUserName().Equals(user)).First();
            game.removeSpectator(spec);
            return game.GetPlayers().ConvertAll(x => (SpectatingUser)x).Union(game.GetSpectators()).Select(player => player.GetUserName()).ToList();
        }

        public List<string> postMessage(string user, string message, int gameID)
        {
            Game game = gc.getGameByID(gameID);
            if (game == null)
                return null;
            
            return game.GetChat().SendMessage(user, message);
        }

        public List<string> postWhisperMessage(string from, string to, string message, int gameID)
        {
            Game game = gc.getGameByID(gameID);
            if (game == null)
                return null;
            return game.GetChat().SendPMMessage(from, to, message);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using ServiceLayer;
using Gaming;
using GameSystem;
using CommunicatoinLayer.Managers;
using System.Threading.Tasks;

namespace CommunicatoinLayer.Hubs
{
    public class GameCenterHub : Hub
    {
        public Game createGame(GamePreferences preferecnces , UserProfile user)
        {
            GameCenterInterface gc = new GameCenterService();
            return gc.createGame(preferecnces , user);
        }

        List<Game> getActiveGames(String criterion, object param, UserProfile user)
        {
            GameCenterInterface gc = new GameCenterService();
            return gc.getActiveGames(criterion , param , user);
        }

        public List<List<Move>> getAllReplayesOfInActiveGames()
        {
            GameCenterInterface gc = new GameCenterService();
            return gc.getAllReplayesOfInActiveGames();
        }

        public List<Game> getAllSpectatingGames()
        {
            GameCenterInterface gc = new GameCenterService();
            return gc.getAllSpectatingGames();
        }

        public async Task<bool> joinGame(Game game, UserProfile u, int credit)
        {
            GameCenterInterface gc = new GameCenterService();
            if (gc.joinGame(game, u, credit))
            {
                int gameId = 0; //TODO :  game.getGameId();
                GameCenterManager.Instance.joinGame(u.Username, gameId);
                await Groups.Add(Context.ConnectionId, "game " + gameId);
                return true;
            }

            return false;
        }

        public async Task<bool> spectateGame(Game game, UserProfile u)
        {
            GameCenterInterface gc = new GameCenterService();
            int gameId = 0; // TODO : game.getGameId();
            GameCenterManager.Instance.spectateGame(u.Username, gameId);
            await Groups.Add(Context.ConnectionId, "game " + gameId);
            return gc.spectateGame(game, u);
        }

    }
}
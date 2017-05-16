using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using ServiceLayer;
using GameSystem;
using CommunicatoinLayer.Managers;
using System.Threading.Tasks;
using ServiceLayer.Models;
using Gaming;

namespace CommunicatoinLayer.Hubs
{
    public class GameCenterHub : Hub
    {
        public ClientGame createGame(GamePreferences preferecnces , string userName)
        {
            GameCenterService gc = new GameCenterService();
            return gc.createGame(preferecnces , userName);
        }

        List<ClientGame> getActiveGames(string criterion, object param, string userName)
        {
            GameCenterService gc = new GameCenterService();
            return gc.getActiveGames(criterion , param , userName);
        }

        public List<List<Move>> getAllReplayesOfInActiveGames()
        {
            GameCenterService gc = new GameCenterService();
            return gc.getAllReplayesOfInActiveGames();
        }

        public List<ClientGame> getAllSpectatingGames()
        {
            GameCenterService gc = new GameCenterService();
            return gc.getAllSpectatingGames();
        }

        public async Task<bool> joinGame(ClientGame game, string userName, int credit)
        {
            GameCenterService gc = new GameCenterService();
            List<string> usersToSend = new List<string>();
            if ((usersToSend = gc.joinGame(game.getID(), userName, credit)) != null)
            {
                int gameId = game.getID();
                GameCenterManager.Instance.joinGame(userName, gameId);
                await Groups.Add(Context.ConnectionId, "game " + gameId);
                Clients.Clients(usersToSend.Select(user => AuthManager.Instance.GetConnectionIdByName(user)).ToList()).joinGame(game.getID(), userName);
                return true;
            }

            return false;
        }

        public async Task<bool> spectateGame(ClientGame game, string userName)
        {
            GameCenterService gc = new GameCenterService();
            List<string> usersToSend = new List<string>();
            if((usersToSend = gc.spectateGame(game.getID(), userName)) != null)
            {
                int gameId = game.getID();
                GameCenterManager.Instance.spectateGame(userName, gameId);
                await Groups.Add(Context.ConnectionId, "game " + gameId);
                Clients.Clients(usersToSend.Select(user => AuthManager.Instance.GetConnectionIdByName(user)).ToList()).spectateGame(game.getID(), userName);
                return true;
            }

            return false;
        }

        public bool unknownUserEditLeague(string userName, int minimumLeagueRank)
        {
            GameCenterService gc = new GameCenterService();
            return gc.unknownUserEditLeague(userName, minimumLeagueRank);
        }

    }
}
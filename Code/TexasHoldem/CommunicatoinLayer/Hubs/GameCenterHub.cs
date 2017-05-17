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
        public ClientGame createGame(GamePreferences preferecnces)
        {
            string userName = AuthManager.Instance.GetNameByConnectionId(Context.ConnectionId);
            GameCenterService gc = new GameCenterService();
            return gc.createGame(preferecnces , userName);
        }

        List<ClientGame> getActiveGames(string criterion, object param)
        {
            string userName = AuthManager.Instance.GetNameByConnectionId(Context.ConnectionId);
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

        public async Task<ClientGame> joinGame(int gameId, int credit)
        {
            string userName = AuthManager.Instance.GetNameByConnectionId(Context.ConnectionId);
            GameCenterService gc = new GameCenterService();
            List<string> usersToSend = new List<string>();
            ClientGame game;
            if ((usersToSend = gc.joinGame(gameId, userName, credit , out game)) != null)
            {
                GameCenterManager.Instance.joinGame(userName, gameId);
                await Groups.Add(Context.ConnectionId, "game " + gameId);
                Clients.Clients(usersToSend.Select(user => AuthManager.Instance.GetConnectionIdByName(user)).ToList()).joinGame(gameId, userName);
                return game;
            }

            return null;
        }

        public async Task<bool> spectateGame(int gameId)
        {
            string userName = AuthManager.Instance.GetNameByConnectionId(Context.ConnectionId);
            GameCenterService gc = new GameCenterService();
            List<string> usersToSend = new List<string>();
            if((usersToSend = gc.spectateGame(gameId, userName)) != null)
            {
                GameCenterManager.Instance.spectateGame(userName, gameId);
                await Groups.Add(Context.ConnectionId, "game " + gameId);
                Clients.Clients(usersToSend.Select(user => AuthManager.Instance.GetConnectionIdByName(user)).ToList()).spectateGame(gameId, userName);
                return true;
            }

            return false;
        }

        public bool unknownUserEditLeague(int minimumLeagueRank)
        {
            string userName = AuthManager.Instance.GetNameByConnectionId(Context.ConnectionId);
            GameCenterService gc = new GameCenterService();
            return gc.unknownUserEditLeague(userName, minimumLeagueRank);
        }

    }
}
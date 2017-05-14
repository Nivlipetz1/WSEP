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
        public ClientGame createGame(GamePreferences preferecnces , ClientUserProfile user)
        {
            GameCenterService gc = new GameCenterService();
            return gc.createGame(preferecnces , user);
        }

        List<ClientGame> getActiveGames(string criterion, object param, ClientUserProfile user)
        {
            GameCenterService gc = new GameCenterService();
            return gc.getActiveGames(criterion , param , user);
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

        public async Task<bool> joinGame(ClientGame game, ClientUserProfile u, int credit)
        {
            GameCenterService gc = new GameCenterService();
            List<string> usersToSend = new List<string>();
            if ((usersToSend = gc.joinGame(game.getID(), u, credit)) != null)
            {
                int gameId = game.getID();
                GameCenterManager.Instance.joinGame(u.Username, gameId);
                await Groups.Add(Context.ConnectionId, "game " + gameId);
                Clients.Clients(usersToSend.Select(user => AuthManager.Instance.GetConnectionIdByName(user)).ToList()).joinGame(game.getID(), u);
                return true;
            }

            return false;
        }

        public async Task<bool> spectateGame(ClientGame game, ClientUserProfile u)
        {
            GameCenterService gc = new GameCenterService();
            int gameId = game.getID();
            GameCenterManager.Instance.spectateGame(u.Username, gameId);
            await Groups.Add(Context.ConnectionId, "game " + gameId);
            List<string> usersToSend = new List<string>();

            if((usersToSend = gc.spectateGame(game.getID(), u)) != null)
                Clients.Clients(usersToSend.Select(user => AuthManager.Instance.GetConnectionIdByName(user)).ToList()).spectateGame(game.getID(), u);

            return false;
        }

    }
}
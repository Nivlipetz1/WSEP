﻿using System;
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
            if (!AuthManager.Instance.containsConnection(Context.ConnectionId))
                return null;
            string userName = AuthManager.Instance.GetNameByConnectionId(Context.ConnectionId);
            GameCenterService gc = new GameCenterService();
            ClientGame game =  gc.createGame(preferecnces , userName);
            return game;
        }

        public List<ClientGame> getActiveGames(string criterion, object param)
        {
            if (!AuthManager.Instance.containsConnection(Context.ConnectionId))
                return null;
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
            if (!AuthManager.Instance.containsConnection(Context.ConnectionId))
                return null;
            string userName = AuthManager.Instance.GetNameByConnectionId(Context.ConnectionId);
            GameCenterService gc = new GameCenterService();
            List<string> usersToSend = new List<string>();
            if ((usersToSend = gc.joinGame(gameId, userName, credit)) != null)
            {
                GameCenterManager.Instance.joinGame(userName, gameId);
                await Groups.Add(Context.ConnectionId, "game " + gameId);
                Clients.Clients(usersToSend.Select(user => AuthManager.Instance.GetConnectionIdByName(user)).ToList()).joinGame(gameId, userName);
                return gc.getGameById(gameId);
            }

            return null;
        }

        public async Task<bool> spectateGame(int gameId)
        {
            if (!AuthManager.Instance.containsConnection(Context.ConnectionId))
                return false;
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
            if (!AuthManager.Instance.containsConnection(Context.ConnectionId))
                return false;
            string userName = AuthManager.Instance.GetNameByConnectionId(Context.ConnectionId);
            GameCenterService gc = new GameCenterService();
            return gc.unknownUserEditLeague(userName, minimumLeagueRank);
        }

        public ClientGame getGame(int gameId)
        {
            GameCenterService gc = new GameCenterService();
            return gc.getGameById(gameId);
        }

    }
}
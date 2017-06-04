using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using ServiceLayer.Models;
using ServiceLayer;
using CommunicatoinLayer.Managers;
using Microsoft.AspNet.SignalR.Hubs;

namespace CommunicatoinLayer.Hubs
{
    [HubName("GameHub")]
    public class GameHub : Hub
    {
        public bool bet(int gameID, string minimumBet)
        {
            if (!AuthManager.Instance.containsConnection(Context.ConnectionId))
                return false;
            string user = AuthManager.Instance.GetNameByConnectionId(Context.ConnectionId);
            GameService gs = new GameService();
            return gs.bet(user, gameID, minimumBet);
        }

        public bool removePlayer(int gameID)
        {
            if (!AuthManager.Instance.containsConnection(Context.ConnectionId))
                return false;
            string user = AuthManager.Instance.GetNameByConnectionId(Context.ConnectionId);
            GameService gs = new GameService();
            List<string> usersToSend = gs.removePlayer(user, gameID);
            Clients.Clients(usersToSend.Select(u => AuthManager.Instance.GetConnectionIdByName(u)).ToList()).removePlayer(user, gameID);
            return true;
        }

        public bool removeSpectator(int gameID)
        {
            if (!AuthManager.Instance.containsConnection(Context.ConnectionId))
                return false;
            string user = AuthManager.Instance.GetNameByConnectionId(Context.ConnectionId);
            GameService gs = new GameService();
            List<string> usersToSend = gs.removeSpectator(user, gameID);
            Clients.Clients(usersToSend.Select(u => AuthManager.Instance.GetConnectionIdByName(u)).ToList()).removeSpectator(user, gameID);
            return true;
        }

        public bool postMessage(string message, int gameID)
        {
            if (!AuthManager.Instance.containsConnection(Context.ConnectionId))
                return false;
            string user = AuthManager.Instance.GetNameByConnectionId(Context.ConnectionId);
            GameService gs = new GameService();
            List<string> usersToSend = gs.postMessage(user, message , gameID);
            Clients.Clients(usersToSend.Select(u => AuthManager.Instance.GetConnectionIdByName(u)).ToList()).pushMessage(user, message , gameID);
            return true;
        }

        public bool postWhisperMessage(string to, string message, int gameID)
        {
            if (!AuthManager.Instance.containsConnection(Context.ConnectionId))
                return false;
            string from = AuthManager.Instance.GetNameByConnectionId(Context.ConnectionId);
            GameService gs = new GameService();
            List<string> usersToSend = gs.postWhisperMessage(from , to , message, gameID);
            usersToSend.Remove(from);
            Clients.Clients(usersToSend.Select(u => AuthManager.Instance.GetConnectionIdByName(u)).ToList()).pushWhisperMessage(from, message, gameID);
            return true;
        }
    }
}
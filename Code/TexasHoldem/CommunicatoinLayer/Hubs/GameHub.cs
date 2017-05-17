using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using ServiceLayer.Models;
using ServiceLayer;
using CommunicatoinLayer.Managers;

namespace CommunicatoinLayer.Hubs
{
    public class GameHub : Hub
    {
        public bool bet(int gameID, string minimumBet)
        {
            string user = AuthManager.Instance.GetNameByConnectionId(Context.ConnectionId);
            GameService gs = new GameService();
            return gs.bet(user, gameID, minimumBet);
        }

        public bool removePlayer(int gameID)
        {
            string user = AuthManager.Instance.GetNameByConnectionId(Context.ConnectionId);
            GameService gs = new GameService();
            List<string> usersToSend = gs.removePlayer(user, gameID);
            Clients.Clients(usersToSend.Select(u => AuthManager.Instance.GetConnectionIdByName(u)).ToList()).removePlayer(user, gameID);
            return true;
        }

        public bool removeSpectator(int gameID)
        {
            string user = AuthManager.Instance.GetNameByConnectionId(Context.ConnectionId);
            GameService gs = new GameService();
            List<string> usersToSend = gs.removeSpectator(user, gameID);
            Clients.Clients(usersToSend.Select(u => AuthManager.Instance.GetConnectionIdByName(u)).ToList()).removeSpectator(user, gameID);
            return true;
        }

        public bool postMessage(string message, int gameID)
        {
            string user = AuthManager.Instance.GetNameByConnectionId(Context.ConnectionId);
            GameService gs = new GameService();
            List<string> usersToSend = gs.postMessage(user, message , gameID);
            usersToSend.Remove(user);
            Clients.Clients(usersToSend.Select(u => AuthManager.Instance.GetConnectionIdByName(u)).ToList()).postMessage(user, message , gameID);
            return true;
        }

        public bool postWhisperMessage(string to, string message, int gameID)
        {
            string from = AuthManager.Instance.GetNameByConnectionId(Context.ConnectionId);
            GameService gs = new GameService();
            List<string> usersToSend = gs.postWhisperMessage(from , to , message, gameID);
            usersToSend.Remove(from);
            Clients.Clients(usersToSend.Select(u => AuthManager.Instance.GetConnectionIdByName(u)).ToList()).postMessage(from, message, gameID);
            return true;
        }
    }
}
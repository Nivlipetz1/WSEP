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
        public bool bet(string user, int gameID, string minimumBet)
        {
            GameService gs = new GameService();
            return gs.bet(user, gameID, minimumBet);
        }

        public bool removePlayer(string user, int gameID)
        {
            GameService gs = new GameService();
            List<string> usersToSend = gs.removePlayer(user, gameID);
            Clients.Clients(usersToSend.Select(u => AuthManager.Instance.GetConnectionIdByName(u)).ToList()).removePlayer(user, gameID);
            return true;
        }

        public bool removeSpectator(string user, int gameID)
        {
            GameService gs = new GameService();
            List<string> usersToSend = gs.removeSpectator(user, gameID);
            Clients.Clients(usersToSend.Select(u => AuthManager.Instance.GetConnectionIdByName(u)).ToList()).removeSpectator(user, gameID);
            return true;
        }

        public bool postMessage(string user, string message, int gameID)
        {
            GameService gs = new GameService();
            List<string> usersToSend = gs.postMessage(user, message , gameID);
            usersToSend.Remove(user);
            Clients.Clients(usersToSend.Select(u => AuthManager.Instance.GetConnectionIdByName(u)).ToList()).postMessage(user, message , gameID);
            return true;
        }

        public bool postWhisperMessage(string from, string to, string message, int gameID)
        {
            GameService gs = new GameService();
            List<string> usersToSend = gs.postWhisperMessage(from , to , message, gameID);
            usersToSend.Remove(from);
            Clients.Clients(usersToSend.Select(u => AuthManager.Instance.GetConnectionIdByName(u)).ToList()).postMessage(from, message, gameID);
            return true;
        }
    }
}
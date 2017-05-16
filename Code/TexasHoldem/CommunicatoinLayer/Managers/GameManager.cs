using CommunicatoinLayer.Hubs;
using GameSystem;
using Gaming;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Infrastructure;
using ServiceLayer.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommunicatoinLayer.Managers
{
    public class GameManager
    {
        private static Lazy<GameManager> LazyInstance = new Lazy<GameManager>(() => new GameManager(GlobalHost.ConnectionManager), true);
        private IHubConnectionContext<dynamic> Clients { set; get; }

        private GameManager(IConnectionManager connectionManager)
        {
            Clients = connectionManager.GetHubContext<GameHub>().Clients;
            NotificationService.pushMoveEvt += pushMove;
        }

        public static GameManager Instance
        {
            get { return LazyInstance.Value; }
        }

        public void pushMove(List<string> userNames, Move move, int gameId)
        {
            List<string> connectionIds = userNames.Select(user => AuthManager.Instance.GetConnectionIdByName(user)).ToList();
            Clients.Clients(connectionIds).pushMove(move, gameId);
        }
    }
}
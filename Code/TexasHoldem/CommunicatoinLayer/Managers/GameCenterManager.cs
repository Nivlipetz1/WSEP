using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommunicatoinLayer.Hubs;

namespace CommunicatoinLayer.Managers
{
    public class GameCenterManager
    {
        private static Lazy<GameCenterManager> LazyInstance = new Lazy<GameCenterManager>(() => new GameCenterManager(GlobalHost.ConnectionManager), true);
        private readonly ConcurrentDictionary<int, ConcurrentBag<string>> usersPerGame;
        private readonly ConcurrentDictionary<int, ConcurrentBag<string>> spectatorsPerGame;
        private IHubConnectionContext<dynamic> Clients { set; get; }

        private GameCenterManager(IConnectionManager connectionManager)
        {
            Clients = connectionManager.GetHubContext<GameCenterHub>().Clients;
            usersPerGame = new ConcurrentDictionary<int, ConcurrentBag<string>>();
            spectatorsPerGame = new ConcurrentDictionary<int, ConcurrentBag<string>>();
        }

        public static GameCenterManager Instance
        {
            get { return LazyInstance.Value; }
        }

        public void joinGame(string userName, int gameId)
        {
            ConcurrentBag<string> usersInRoom;
            if (usersPerGame.TryGetValue(gameId, out usersInRoom))
            {
                usersInRoom.Add(userName);
                return;
            }

            usersInRoom = new ConcurrentBag<string>();
            usersInRoom.Add(userName);
            usersPerGame[gameId] = usersInRoom;

        }

        public void spectateGame(string userName, int gameId)
        {
            ConcurrentBag<string> spectatorsInRoom;
            if (spectatorsPerGame.TryGetValue(gameId, out spectatorsInRoom))
            {
                spectatorsInRoom.Add(userName);
                return;
            }

            spectatorsInRoom = new ConcurrentBag<string>();
            spectatorsInRoom.Add(userName);
            spectatorsPerGame[gameId] = spectatorsInRoom;
        }
    }
}
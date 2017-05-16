using GUI.Models;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Communication
{
    public class GameFunctions
    {
        private static Lazy<GameFunctions> LazyInstance = new Lazy<GameFunctions>(() => new GameFunctions(), true);
        private IHubProxy gameHubProxy;

        private GameFunctions()
        {

        }

        public static GameFunctions Instance
        {
            get { return LazyInstance.Value; }
        }

        public IHubProxy GameHubProxy
        {
            get { return gameHubProxy; }

            set { gameHubProxy = value; }
        }

        public void initOnFunctions()
        {
            gameHubProxy.On<Move , int>("pushMove", (move , gameID) =>
            {
            });

            gameHubProxy.On<string, int>("removePlayer", (user, gameID) =>
            {
            });

            gameHubProxy.On<string, int>("removeSpectator", (user, gameID) =>
            {
            });

            gameHubProxy.On<string, string , int>("postMessage", (user, message , gameID) =>
            {
            });

            gameHubProxy.On<List<string>, int>("pushWinners", (winners, gameID) =>
            {
            });
        }

        public bool bet(string user, int gameID, string minimumBet)
        {
            Task<bool> res = gameHubProxy.Invoke<bool>("bet", user, gameID , minimumBet);
            res.Wait();
            return res.Result;
        }

        public bool removePlayer(string user, int gameID)
        {
            Task<bool> res = gameHubProxy.Invoke<bool>("removePlayer", user, gameID);
            res.Wait();
            return res.Result;
        }

        public bool removeSpectator(string user, int gameID)
        {
            Task<bool> res = gameHubProxy.Invoke<bool>("removeSpectator", user, gameID);
            res.Wait();
            return res.Result;
        }

        public bool postMessage(string user, string message, int gameID)
        {
            Task<bool> res = gameHubProxy.Invoke<bool>("postMessage", user, message ,  gameID);
            res.Wait();
            return res.Result;
        }

        public bool postWhisperMessage(string from, string to, string message, int gameID)
        {
            Task<bool> res = gameHubProxy.Invoke<bool>("postWhisperMessage", from, to , message, gameID);
            res.Wait();
            return res.Result;
        }
    }
}

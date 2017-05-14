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
            gameHubProxy.On<ClientUserProfile, int , string>("bet", (user , gameID , minimumBet) =>
            {
            });

            gameHubProxy.On<ClientUserProfile, int>("removePlayer", (user, gameID) =>
            {
            });

            gameHubProxy.On<ClientUserProfile, int>("removeSpectator", (user, gameID) =>
            {
            });

            gameHubProxy.On<ClientUserProfile, string , int>("postMessage", (user, message , gameID) =>
            {
            });
        }

        public bool bet(ClientUserProfile user, int gameID, string minimumBet)
        {
            Task<bool> res = gameHubProxy.Invoke<bool>("bet", user, gameID , minimumBet);
            res.Wait();
            return res.Result;
        }

        public bool removePlayer(ClientUserProfile user, int gameID)
        {
            Task<bool> res = gameHubProxy.Invoke<bool>("removePlayer", user, gameID);
            res.Wait();
            return res.Result;
        }

        public bool removeSpectator(ClientUserProfile user, int gameID)
        {
            Task<bool> res = gameHubProxy.Invoke<bool>("removeSpectator", user, gameID);
            res.Wait();
            return res.Result;
        }

        public bool postMessage(ClientUserProfile user, string message, int gameID)
        {
            Task<bool> res = gameHubProxy.Invoke<bool>("postMessage", user, message ,  gameID);
            res.Wait();
            return res.Result;
        }

        public bool postWhisperMessage(ClientUserProfile from, ClientUserProfile to, string message, int gameID)
        {
            Task<bool> res = gameHubProxy.Invoke<bool>("postWhisperMessage", from, to , message, gameID);
            res.Wait();
            return res.Result;
        }
    }
}

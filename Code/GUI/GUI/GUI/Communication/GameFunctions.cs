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
            gameHubProxy.On<string , int>("pushMove", (serializeMove, gameID) =>
            {
                Move move = MoveTypesConverter.deserializeObject<Move>(serializeMove);
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

            gameHubProxy.On<int,int>("yourTurn", (minimumBet , gameId) =>
            {
            });

            gameHubProxy.On<PlayerHand, int>("setHand", (playerHand, gameId) =>
            {
            });
        }

        public bool bet(int gameID, string minimumBet)
        {
            Task<bool> res = gameHubProxy.Invoke<bool>("bet", gameID , minimumBet);
            res.Wait();
            return res.Result;
        }

        public bool removePlayer(int gameID)
        {
            Task<bool> res = gameHubProxy.Invoke<bool>("removePlayer", gameID);
            res.Wait();
            return res.Result;
        }

        public bool removeSpectator(int gameID)
        {
            Task<bool> res = gameHubProxy.Invoke<bool>("removeSpectator", gameID);
            res.Wait();
            return res.Result;
        }

        public bool postMessage(string message, int gameID)
        {
            Task<bool> res = gameHubProxy.Invoke<bool>("postMessage", message ,  gameID);
            res.Wait();
            return res.Result;
        }

        public bool postWhisperMessage(string to, string message, int gameID)
        {
            Task<bool> res = gameHubProxy.Invoke<bool>("postWhisperMessage", to , message, gameID);
            res.Wait();
            return res.Result;
        }
    }
}

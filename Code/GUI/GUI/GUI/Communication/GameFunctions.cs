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

            gameHubProxy.On<string, string , int>("pushMessage", (user, message , gameID) =>
            {
            });

            gameHubProxy.On<string, string, int>("pushWhisperMessage", (from, message, gameID) =>
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

        public async Task<bool> bet(int gameID, string minimumBet)
        {
            return await gameHubProxy.Invoke<bool>("bet", gameID , minimumBet);
        }

        public async Task<bool> removePlayer(int gameID)
        {
            return await gameHubProxy.Invoke<bool>("removePlayer", gameID);
        }

        public async Task<bool> removeSpectator(int gameID)
        {
            return await gameHubProxy.Invoke<bool>("removeSpectator", gameID);
        }

        public async Task<bool> postMessage(string message, int gameID)
        {
            return await gameHubProxy.Invoke<bool>("postMessage", message ,  gameID);
        }

        public async Task<bool> postWhisperMessage(string to, string message, int gameID)
        {
            return await gameHubProxy.Invoke<bool>("postWhisperMessage", to , message, gameID);
        }
    }
}

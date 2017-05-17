using GUI.Models;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Communication
{
    public class GameCenterFunctions
    {
        private static Lazy<GameCenterFunctions> LazyInstance = new Lazy<GameCenterFunctions>(() => new GameCenterFunctions(), true);
        private IHubProxy gameCenterHubProxy;

        private GameCenterFunctions()
        {

        }

        public static GameCenterFunctions Instance
        {
            get { return LazyInstance.Value; }
        }

        public IHubProxy GameCenterHubProxy
        {
            get { return gameCenterHubProxy; }

            set { gameCenterHubProxy = value; }
        }

        public void initOnFunctions()
        {
            gameCenterHubProxy.On<int , string>("joinGame", (gameID , userName) =>
            {
            });

            gameCenterHubProxy.On<int, string>("spectateGame", (gameID , userName) =>
            {
            });
        }

        public ClientGame createGame(GamePreferences preferecnces)
        {
            Task<ClientGame> res = gameCenterHubProxy.Invoke<ClientGame>("createGame", preferecnces);
            res.Wait();
            return res.Result;
        }

        public List<ClientGame> getActiveGames(string criterion, object param)
        {
            Task<List<ClientGame>> res = gameCenterHubProxy.Invoke<List<ClientGame>>("getActiveGames", criterion, param);
            res.Wait();
            return res.Result;
        }

        public List<List<Move>> getAllReplayesOfInActiveGames()
        {
            Task<List<List<Move>>> res = gameCenterHubProxy.Invoke<List<List<Move>>>("getAllReplayesOfInActiveGames");
            res.Wait();
            return res.Result;
        }

        public List<ClientGame> getAllSpectatingGames()
        {
            Task<List<ClientGame>> res = gameCenterHubProxy.Invoke<List<ClientGame>>("getAllSpectatingGames");
            res.Wait();
            return res.Result;
        }

        public ClientGame joinGame(int gameId, int credit)
        {
            Task<ClientGame> res = gameCenterHubProxy.Invoke<ClientGame>("joinGame", gameId, credit);
            res.Wait();
            return res.Result;
        }

        public bool spectateGame(int gameId)
        {
            Task<bool> res = gameCenterHubProxy.Invoke<bool>("spectateGame", gameId);
            res.Wait();
            return res.Result;
        }

        public bool unknownUserEditLeague(int minimumLeagueRank)
        {
            Task<bool> res = gameCenterHubProxy.Invoke<bool>("unknownUserEditLeague", minimumLeagueRank);
            res.Wait();
            return res.Result;
        }
    }
}

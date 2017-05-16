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

        public ClientGame createGame(GamePreferences preferecnces, string user)
        {
            Task<ClientGame> res = gameCenterHubProxy.Invoke<ClientGame>("createGame", preferecnces, user);
            res.Wait();
            return res.Result;
        }

        public List<ClientGame> getActiveGames(string criterion, object param, string user)
        {
            Task<List<ClientGame>> res = gameCenterHubProxy.Invoke<List<ClientGame>>("getActiveGames", criterion, param , user);
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

        public bool joinGame(ClientGame game, string u, int credit)
        {
            Task<bool> res = gameCenterHubProxy.Invoke<bool>("joinGame", game, u , credit);
            res.Wait();
            return res.Result;
        }

        public bool spectateGame(ClientGame game, string u)
        {
            Task<bool> res = gameCenterHubProxy.Invoke<bool>("spectateGame", game, u);
            res.Wait();
            return res.Result;
        }

        public bool unknownUserEditLeague(string user, int minimumLeagueRank)
        {
            Task<bool> res = gameCenterHubProxy.Invoke<bool>("unknownUserEditLeague", user, minimumLeagueRank);
            res.Wait();
            return res.Result;
        }
    }
}

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
        public ServerToClientFunctions serverToClient { get; set; }

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
            gameCenterHubProxy.On<int , ClientUserProfile>("joinGame", (gameID , userProfile) =>
            {
                
            });

            gameCenterHubProxy.On<int, ClientUserProfile>("spectateGame", (gameID , userProfile) =>
            {
            });
        }

        public async Task<ClientGame> createGame(GamePreferences preferecnces)
        {
            return await gameCenterHubProxy.Invoke<ClientGame>("createGame", preferecnces);
        }

        public async Task<List<ClientGame>> getActiveGames(string criterion, object param)
        {
            return await gameCenterHubProxy.Invoke<List<ClientGame>>("getActiveGames", criterion, param);
        }

        public async Task<List<List<Move>>> getAllReplayesOfInActiveGames()
        {
            string res = await gameCenterHubProxy.Invoke<string>("getAllReplayesOfInActiveGames");
            return MoveTypesConverter.deserializeObject<List<List<Move>>>(res);
        }

        public async Task<List<ClientGame>> getAllSpectatingGames()
        {
            return await gameCenterHubProxy.Invoke<List<ClientGame>>("getAllSpectatingGames");
        }

        public async Task<ClientGame> joinGame(int gameId, int credit)
        {
            return await gameCenterHubProxy.Invoke<ClientGame>("joinGame", gameId, credit);
        }

        public async Task<bool> spectateGame(int gameId)
        {
            return await gameCenterHubProxy.Invoke<bool>("spectateGame", gameId);
        }

        public async Task<bool> unknownUserEditLeague(int minimumLeagueRank)
        {
            return await gameCenterHubProxy.Invoke<bool>("unknownUserEditLeague", minimumLeagueRank);
        }

        public async Task<ClientGame> getGame(int gameId)
        {
            return await gameCenterHubProxy.Invoke<ClientGame>("getGame", gameId);
        }
    }
}

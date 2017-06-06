using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gaming;
using GameSystem;
using ServiceLayer.Models;

namespace ServiceLayer.Interfaces
{
    public interface GCServiceInterface
    {
        ClientGame createGame(GamePreferences preferecnces, String  user);

        List<ClientGame> getAllSpectatingGames();

        List<ClientGame> getActiveGames(String criterion, object param, String user);

        List<String> joinGame(int gameID, String u, int credit);

        List<string> spectateGame(int gameID, String u);

        List<List<Move>> getAllReplayesOfInActiveGames();

        ClientGame getGameById(int gameId);

        List<Move> getReplayByGameId(int gameId);
    }
}

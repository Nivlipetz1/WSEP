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
        ClientGame createGame(GamePreferences preferecnces, ClientUserProfile  user);

        List<ClientGame> getAllSpectatingGames();

        List<ClientGame> getActiveGames(String criterion, object param, ClientUserProfile user);

        List<String> joinGame(int gameID, ClientUserProfile u, int credit);

        List<string> spectateGame(int gameID, ClientUserProfile u);

        List<List<Move>> getAllReplayesOfInActiveGames();
    }
}

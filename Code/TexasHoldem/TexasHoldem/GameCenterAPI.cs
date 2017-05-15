using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gaming;

namespace GameSystem
{
    public interface GameCenterInterface
    {
        Game createGame(GamePreferences preferecnces , UserProfile user);

        List<Game> getAllSpectatingGames();

        List<Game> getActiveGames(String criterion, object param, UserProfile user);

        bool joinGame(Game game, UserProfile u , int credit);

        bool spectateGame(Game game, UserProfile u);

        List<List<Move>> getAllReplayesOfInActiveGames();

        Game getGameByID(int gameID);

        bool unknownUserEditLeague(UserProfile user, League league);

        League getLeagueByID(int id);
    }
}

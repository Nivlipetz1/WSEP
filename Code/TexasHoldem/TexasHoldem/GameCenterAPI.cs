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
        Game createGame(GamePreferences preferecnces);

        List<Game> getAllSpectatingGames();

        List<Game> getAllActiveGamesByPlayerName(String playerName);

        List<Game> getAllActiveGamesByPotSize(int potSize);

        List<Game> getAllActiveGamesByGamePreference(GamePreferences preferences);

        bool joinGame(Game game, UserProfile u , int credit);

        bool spectateGame(Game game, UserProfile u);

        List<List<Move>> getAllReplayesOfInActiveGames();
    }
}

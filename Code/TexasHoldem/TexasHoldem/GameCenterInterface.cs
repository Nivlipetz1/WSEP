using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gaming;
using User;

namespace TexasHoldemSystem
{
    interface GameCenterInterface
    {
        bool createGame(GamePreferences preferecnces, UserProfile creatingUser, int buyIn);

        List<Game> getAllSpectatingGames(UserProfile u);

        List<Game> getAllActiveGamesByPlayerName(String playerName);

        List<Game> getAllActiveGamesByPotSize(int potSize);

        List<Game> getAllActiveGamesByGamePreference(GamePreferences preferences);

        void joinGame(Game game, UserProfile u);

        void spectateGame(Game game, UserProfile u);
    }
}

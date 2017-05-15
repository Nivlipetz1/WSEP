using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gaming;

namespace GameSystem
{
    public interface LeagueAPI
    {
        bool createNewLeague(int minimumRank);
        bool removeUserFromLeague(UserProfile user, League league);
        League getLeagueByUser(UserProfile user);
        League getLeagueByRank(int Rank);
        Dictionary<int, League> getLeagues();
        void updateLeagueToUser(PlayingUser user);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameUtilities;

namespace Services
{
    public interface LeagueAPI
    {
        bool createNewLeague(int minimumRank);
        bool addUserToLeague(UserProfile user, League league);
        bool removeUserFromLeague(UserProfile user, League league);
        bool changeLeagueMinimumRank(League league, int newRank);
        League getLeagueByUser(UserProfile user);
        League getLeagueByRank(int Rank);
        Dictionary<int, League> getLeagues();
        void updateLeagueToUser(UserProfile user);
    }
}

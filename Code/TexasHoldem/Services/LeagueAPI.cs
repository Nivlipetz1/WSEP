using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameUtilities;

namespace TexasHoldemSystem
{
    interface LeagueAPI
    {
        bool createNewLeague(int minimumRank);
        bool addUserToLeague(UserProfile user, LeagueAPI league);
        bool removeUserFromLeague(UserProfile user, LeagueAPI league);
        bool changeLeagueMinimumRank(League league, int newRank);
        League getLeagueByUser(UserProfile user);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User;

namespace TexasHoldemSystem
{
    interface LeagueApi
    {
        bool addUserToLeague(UserProfile user, League league);
        bool removeUserFormLeague(UserProfile user, League league);
        League getLeagueByUser(UserProfile user);
        League CreateNewLeague(int minimumRank);

    }
}

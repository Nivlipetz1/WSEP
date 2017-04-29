using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldemSystem;
using GameUtilities;

namespace ServiceLayer
{
    public class LeagueService : LeagueAPI
    {
        LeagueAPI league = new GameCenter();

        public bool addUserToLeague(UserProfile user, League league)
        {
            return this.league.addUserToLeague(user, league);
        }

        public bool changeLeagueMinimumRank(League league, int newRank)
        {
            return this.league.changeLeagueMinimumRank(league, newRank);
        }

        public bool createNewLeague(int minimumRank)
        {
            return league.createNewLeague(minimumRank);
        }

        public League getLeagueByRank(int Rank)
        {
            return league.getLeagueByRank(Rank);
        }

        public League getLeagueByUser(UserProfile user)
        {
            return league.getLeagueByUser(user);
        }

        public Dictionary<int, League> getLeagues()
        {
            return league.getLeagues();
        }

        public bool removeUserFromLeague(UserProfile user, League league)
        {
            return this.league.removeUserFromLeague(user, league);
        }

        public void updateLeagueToUser(UserProfile user)
        {
            league.updateLeagueToUser(user);
        }
    }
}

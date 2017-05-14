using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using GameSystem;
using ServiceLayer;
using Gaming;

namespace CommunicatoinLayer.Hubs
{
    public class LeagueHub : Hub
    {
        public bool addUserToLeague(UserProfile user, League league)
        {
            LeagueAPI leagueService = new LeagueService();
            return leagueService.addUserToLeague(user, league);
        }

        public bool changeLeagueMinimumRank(League league, int newRank)
        {
            LeagueAPI leagueService = new LeagueService();
            return leagueService.changeLeagueMinimumRank(league, newRank);
        }

        public bool createNewLeague(int minimumRank)
        {
            LeagueAPI leagueService = new LeagueService();
            return leagueService.createNewLeague(minimumRank);
        }

        public League getLeagueByRank(int Rank)
        {
            LeagueAPI leagueService = new LeagueService();
            return leagueService.getLeagueByRank(Rank);
        }

        public League getLeagueByUser(UserProfile user)
        {
            LeagueAPI leagueService = new LeagueService();
            return leagueService.getLeagueByUser(user);
        }

        public Dictionary<int, League> getLeagues()
        {
            LeagueAPI leagueService = new LeagueService();
            return leagueService.getLeagues();
        }

        public bool removeUserFromLeague(UserProfile user, League league)
        {
            LeagueAPI leagueService = new LeagueService();
            return leagueService.removeUserFromLeague(user, league);
        }

        public void updateLeagueToUser(PlayingUser user)
        {
            LeagueAPI leagueService = new LeagueService();
            leagueService.updateLeagueToUser(user);
        }
    }
}
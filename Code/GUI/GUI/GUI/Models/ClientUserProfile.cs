using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Models
{
    public class ClientUserProfile
    {
        private string username;
        private byte [] avatar;
        private int credit;
        private int leagueId;
        private Statistics userStat;


        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public byte [] Avatar
        {
            get { return avatar; }
        }

        public int Credit
        {
            get { return credit; }
        }

        public int LeagueId
        {
            get { return leagueId; }

        }

        public Statistics UserStat
        {
            get { return userStat; }
        }
    }
}


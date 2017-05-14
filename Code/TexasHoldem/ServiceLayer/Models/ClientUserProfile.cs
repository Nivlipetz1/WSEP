using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameSystem;

namespace ServiceLayer.Models
{
    public class ClientUserProfile
    {
        private string username;
        private Image avatar;
        private int credit;
        private League league;
        private Statistics userStat;

        public ClientUserProfile(UserProfile userProfile)
        {
            username = userProfile.Username;
            avatar = userProfile.Avatar;
            credit = userProfile.Credit;
            league = userProfile.League;
            userStat = userProfile.UserStat;
        }

        public string Username
        {
            get{ return username;}
        }

        public Image Avatar
        {
            get {return avatar;}
        }

        public int Credit
        {
            get { return credit; }
        }

        public League League
        {
            get { return league; }

        }

        public Statistics UserStat
        {
            get { return userStat; }
        }      
    }
}

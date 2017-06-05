using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Gaming;

namespace GameSystem
{
    public class UserProfile
    {
        private string username;
        private string password;
        private Image avatar;
        private int credit;
        private League league;
        private Statistics userStat;

        public string Username
        {
            get{ return username;}
            set{username = value;}
        }

        public string Password
        {
            get{return password;}
            set {password = value; }
        }

        public Image Avatar
        {
            get {return avatar;}
            set {avatar = value;}
        }

        public int Credit
        {
            get { return credit; }
            set { credit = value; }
        }

        public League League
        {
            get
            {
                return league;
            }

            set
            {
                league = value;
            }
        }

        public Statistics UserStat
        {
            get
            {
                return userStat;
            }

            set
            {
                userStat = value;
            }
        }

        public UserProfile(string username, string password)
        {
            this.Username = username;
            this.Password = password;
            userStat = new Statistics();
        }
        public UserProfile(string username,string password,Image avatar)
        {
            this.Username = username;
            this.Password = password;
            this.Avatar = avatar;
            userStat = new Statistics();
        }

        public void setUserLeague(League league)
        {
            this.league = league;
        }

        public void updateStatistics(PlayingUser user)
        {

            int Grosssum = (from g in user.GainPerRound where g >= 0 select g).Sum();
            int roundsPlayed = userStat.Winnings + userStat.Losses;
            
            userStat.Winnings += user.GetRoundsWon();
            userStat.Losses += user.GetRoundsLost();
            userStat.BiggestWin = (userStat.BiggestWin > user.GetMostWon()) ? userStat.BiggestWin : user.GetMostWon();
            userStat.HighestHand = (userStat.HighestHand < user.GetBestHand()) ? userStat.HighestHand : user.GetBestHand();
            userStat.BiggestWallet = (Credit > userStat.BiggestWallet) ? Credit : userStat.BiggestWallet;
            userStat.TotalGrossProfit += Grosssum;
            UserStat.AvgCashGain = (userStat.AvgCashGain * roundsPlayed + user.GainPerRound.Sum()) / (userStat.Winnings + userStat.Losses);
            userStat.AvgGrossProfit = userStat.TotalGrossProfit / (userStat.Winnings + userStat.Losses);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Models
{
    public class ClientGame
    {
        public int id { set; get; }
        public Deck gameDeck { set; get; }
        public List<ClientUserProfile> players { set; get; }
        public List<ClientUserProfile> spectators { set; get; }
        public int[] pot { set; get; }
        public GamePreferences gamePref { set; get; }
        public Dictionary<string, int> playerBets { set; get; }


        public void RemovePlayer(string username)
        {
            foreach(ClientUserProfile prof in players)
            {
                if (prof.username.Equals(username))
                {
                    players.Remove(prof);
                    return;
                }
            }
        }

        public void AddPlayer(ClientUserProfile profile)
        {
            players.Add(profile);
        }
    }
}

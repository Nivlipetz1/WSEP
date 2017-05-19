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
        public IDictionary<ClientUserProfile, int> playerBets { set; get; }
    }
}

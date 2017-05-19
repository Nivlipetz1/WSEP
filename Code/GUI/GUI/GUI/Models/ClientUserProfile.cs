using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Models
{
    public class ClientUserProfile
    {
        public string username { set; get; }
        public byte[] avatar { set; get; }
        public int credit { set; get; }
        public int leagueId { set; get; }
        public Statistics userStat { set; get; }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    class GameDataGrid
    {
        public int ID { get; set; }
        public int PlayersInGame { get; set; }
        public int MaxPlayers { get; set; }
        public int BigBlind { get; set; }
        public int SmallBlind { get; set; }
        public bool SpectatingAllowed { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaming
{
    public class GameLogger
    {
        public int gameID { get; set; }
        public List<Move> gameMoves { get; set; }

        public GameLogger(int gameID)
        {
            this.gameID = gameID;
            gameMoves = new List<Move>();
        }

        public bool AddMove(Move m)
        {
            gameMoves.Add(m);
            NotificationService.Instance.saveReplay(this);
            return true;
        }

        public List<Move> GetMoves()
        {
            return gameMoves;
        }


    }
}

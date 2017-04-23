using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaming
{
    public class GameLogger
    {

        private List<Move> gameMoves;

        public GameLogger()
        {
            gameMoves = new List<Move>();
        }

        public bool AddMove(Move m)
        {
            gameMoves.Add(m);
            return true;
        }

        public List<Move> GetMoves()
        {
            return gameMoves;
        }


    }
}

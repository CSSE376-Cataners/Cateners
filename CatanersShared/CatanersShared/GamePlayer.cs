using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanersShared
{
    public class GamePlayer : Player
    {
        private int victoryPoints;
        private bool isMyTurn;
        //private Dictionary resources
        public GamePlayer(Player player)
            : base(player.Username)
        {

        }

    }
}

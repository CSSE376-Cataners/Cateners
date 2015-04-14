using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanersShared
{
    public class Lobby
    {
        private string GameName;
        private int MaxTimePerTurn;

        public Lobby(string GameName, int MaxTimePerTurn)
        {
            this.GameName = GameName;
            this.MaxTimePerTurn = MaxTimePerTurn;
        }

        public string getGameName()
        {
            return this.GameName;
        }

        public int getTimePer()
        {
            return this.MaxTimePerTurn;
        }
    }
}

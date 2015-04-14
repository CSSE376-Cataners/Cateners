using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanersShared
{
    public class Lobby
    {
        public string GameName;
        public int MaxTimePerTurn;
        public Player Owner;
        public ArrayList players;

        public Lobby(string GameName, int MaxTimePerTurn, Player Owner)
        {
            this.GameName = GameName;
            this.MaxTimePerTurn = MaxTimePerTurn;

            this.players = new ArrayList();
            this.Owner = Owner;
            players.Add(Owner);
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

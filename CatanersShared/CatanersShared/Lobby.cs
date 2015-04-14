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
        private string gameName;
        private int maxTimePerTurn;
        private Player owner;

        public ArrayList Players;

        public string GameName {
            get {
                return gameName;
            }
        }
        public int MaxTimePerTurn {
            get {
                return maxTimePerTurn;
            }
        }
        public Player Owner
        {
            get
            {
                return owner;
            }
        }
        

        public Lobby(string GameName, int MaxTimePerTurn, Player Owner)
        {
            this.gameName = GameName;
            this.maxTimePerTurn = MaxTimePerTurn;

            this.Players = new ArrayList();
            this.owner = Owner;
            Players.Add(Owner);
        }

    }
}

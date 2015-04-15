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
        private int playerCount;

        public ArrayList players;

        public ArrayList Players
        {
            get
            {
                return players;
            }
        }

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
            this.playerCount = 0;
            this.players = new ArrayList();
            this.owner = Owner;
            Players.Add(Owner);
        }

        public void addPlayer(Player newPlayer)
        {
            this.players.Add(newPlayer);
            this.playerCount = this.playerCount + 1;
        }

        public String toJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}

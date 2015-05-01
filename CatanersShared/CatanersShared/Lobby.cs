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
        public int lobbyID;
        public Boolean allReady;

        private List<Player> players;

        public virtual List<Player> Players
        {
            get
            {
                return players;
            }
            set
            {
                players = value;
            }
        }

        public string GameName {
            get {
                return gameName;
            }
            set
            {
                gameName = value;
            }
        }

        public int MaxTimePerTurn {
            get {
                return maxTimePerTurn;
            }
            set
            {
                maxTimePerTurn = value;
            }
        }

        public Player Owner
        {
            get
            {
                return (Player)owner;
            }
            set
            {
                owner = value;
            }
        }

        public int PlayerCount
        {
            get
            {
                return players.Count;
            }
        }
        

        public Lobby(string GameName, int MaxTimePerTurn, Player Owner, int LobbyID)
        {
            this.gameName = GameName;
            this.maxTimePerTurn = MaxTimePerTurn;
            this.players = new List<Player>();
            this.owner = Owner;
            addPlayer(owner);
            this.lobbyID = LobbyID;
        }

        /// <summary>
        /// Used by Json Converter. Do not normaly use.
        /// </summary>
        public Lobby()
        {

        }

        public void addPlayer(Player newPlayer)
        {
            this.players.Add(newPlayer);
        }

        public void removePlayer(Player player)
        {
            this.players.Remove(player);
        }

        public void removeAll()
        {
            this.players.Clear();
        }

        public String toJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static Lobby fromJson(String s)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Lobby>(s);
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Lobby other = (Lobby)obj;
            return other.GameName.Equals(this.GameName) && other.MaxTimePerTurn == this.MaxTimePerTurn && other.Owner.Equals(this.Owner);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return this.GameName.GetHashCode() + this.MaxTimePerTurn.GetHashCode() + this.owner.GetHashCode();
        }

        public static List<Lobby> jsonToLobbyList(String s)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Lobby>>(s);
        }
    }
}

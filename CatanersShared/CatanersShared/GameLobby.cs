using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanersShared
{
    public class GameLobby : Lobby
    {
        public List<GamePlayer> gamePlayers;
        private Dictionary<int, string> colorDict = new Dictionary<int, string>();

        public GameLobby()
        {
            // For Json Deparsing
        }

        public GameLobby(Lobby lobby)
            : base(lobby.GameName, lobby.MaxTimePerTurn, new GamePlayer(lobby.Owner.Username), lobby.lobbyID)
        {
            this.colorDict.Add(0, "Blue");
            this.colorDict.Add(1, "Red");
            this.colorDict.Add(2, "Green");
            this.colorDict.Add(3, "Purple");
            this.gamePlayers = new List<GamePlayer>();
            this.Players.Clear();
            for (int i = 0; i < lobby.PlayerCount; i++)
            {
                GamePlayer newPlayer = new GamePlayer(lobby.Players[i].Username);
                Console.WriteLine("Player" + i.ToString());
                newPlayer.setColor(this.colorDict[i]);
                gamePlayers.Add(newPlayer);
                this.addPlayer(lobby.Players[i]);
            }
        }

        public new String toJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public new static GameLobby fromJson(String s)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<GameLobby>(s);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is GameLobby))
            {
                return false;
            }
            GameLobby other = (GameLobby)obj;
            return other.GameName.Equals(this.GameName) && other.MaxTimePerTurn == this.MaxTimePerTurn && other.Owner.Equals(this.Owner) && Enumerable.SequenceEqual(this.gamePlayers,other.gamePlayers);
        }
    }
}

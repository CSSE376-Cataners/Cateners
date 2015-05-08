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

        public GameLobby()
        {
            // For Json Deparsing
        }

        public GameLobby(Lobby lobby)
            : base(lobby.GameName, lobby.MaxTimePerTurn, new GamePlayer(lobby.Owner.Username), lobby.lobbyID)
        {
            this.gamePlayers = new List<GamePlayer>();
            for (int i = 0; i < lobby.PlayerCount; i++)
            {
                gamePlayers.Add(new GamePlayer(lobby.Players[i].Username));
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

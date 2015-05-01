using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanersShared
{
    public class GameLobby : Lobby
    {
        public List<GamePlayer> Players;

        public GameLobby()
        {
            // For Json Deparsing
        }

        public GameLobby(Lobby lobby)
            : base(lobby.GameName, lobby.MaxTimePerTurn, new GamePlayer(lobby.Owner.Username), lobby.lobbyID)
        {
            this.Players = new List<GamePlayer>();
            Players.Clear();
            for (int i = 0; i < lobby.PlayerCount; i++)
            {
                Players.Add(new GamePlayer(lobby.Players[i].Username));
            }
        }

        public new String toJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public new static Lobby fromJson(String s)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<GameLobby>(s);
        }
    }
}

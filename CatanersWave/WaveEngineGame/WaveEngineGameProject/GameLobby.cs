using CatanersShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveEngineGameProject
{
    class GameLobby:Lobby
    {
        public override List<GamePlayer> Players;
        public GameLobby(Lobby lobby)
            : base(lobby.GameName, lobby.MaxTimePerTurn, new GamePlayer(lobby.Owner.Username), lobby.lobbyID)
        {
            Players.Clear();
            for (int i = 0; i < lobby.PlayerCount; i++)
            {
                Players.Add(new GamePlayer(lobby.Players[i].Username));
            }
        }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanersShared
{
    public class GameLobby : Lobby
    {
        public List<GamePlayer> Players;
        public GameLobby(Lobby lobby)
            : base(lobby.GameName, lobby.MaxTimePerTurn, new GamePlayer(lobby.Owner.Username), lobby.lobbyID)
        {
            this.Players = new List<GamePlayer>();
            
        }
    }
}

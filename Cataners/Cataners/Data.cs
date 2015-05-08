using Cataners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatanersShared;
using WaveEngine.Framework.Services;

namespace Cataners
{
    public static class Data
    {
        public static List<Lobby> Lobbies = new List<Lobby>();
        public static Lobby currentLobby = new Lobby();
        public static String username = "";
        public static GamePlayer currentGamePlayer;
        public static GameLobby currentGameLobby;
        public static GamePlayer currentGameOwner;
        public static bool isMyTurn;
    }
}
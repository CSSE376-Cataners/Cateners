using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatanersShared;

namespace CatenersServer
{
    public class Data
    {
        private static Data instance = new Data();

        public static Data INSTANCE 
        {
            get
            {
                return instance;
            }
        }

        public List<Lobby> Lobbies
        {
            get
            {
                return lobbies;
            }
        }

        private List<Lobby> lobbies = new List<Lobby>();

        public int nextLobbyID = 0;

    }
}

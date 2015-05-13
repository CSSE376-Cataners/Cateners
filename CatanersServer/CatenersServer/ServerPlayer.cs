using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatanersShared;
using Newtonsoft.Json;

namespace CatenersServer
{
    public class ServerPlayer : Player
    {
        [JsonIgnoreAttribute]
        public Client client;
        private int playerNumber;

        public ServerPlayer(String username, Client client) : base(username)
        {
            this.client = client;
            this.playerNumber = 0;
        }

        public void setPlayerNumber(int i)
        {
            this.playerNumber = i;
        }
    }
}

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

        public ServerPlayer(String username, Client client) : base(username)
        {
            this.client = client;
        }
    }
}

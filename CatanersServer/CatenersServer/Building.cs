using CatanersShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatenersServer
{
    class Building
    {
        public List<Hex> hexes;
        public GamePlayer owner;
        public bool isCity = false;

        public Building()
        {
            this.hexes = new List<Hex>();

        }
    }
}

using CatanersShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatenersServer
{
    public class Building
    {
        public List<Hex> hexes;
        public GamePlayer owner;
        public bool isCity = false;

        public Building(Hex hex)
        {
            this.hexes = new List<Hex>();
            this.hexes.Add(hex);
        }
    }
}

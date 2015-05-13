using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatenersServer
{
    public class Board
    {
        public List<Hex> hexes;
        public List<Building> buildings;
        public Robber robber;

        public Board()
        {
            this.hexes = new List<Hex>();
            this.buildings = new List<Building>();
            this.robber = null;
        }
    }

   

}

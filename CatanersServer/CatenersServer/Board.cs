using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatenersServer
{
    public class Board
    {
        public Hex[] hexes;
        public Building[] buildings;
        public Robber robber;

        public Board()
        {
            this.hexes = new Hex[19];
            this.buildings = new Building[54];
            this.robber = null;
            for (int i = 0; i < 54; i++)
            {
                this.buildings[i] = new Building();
            }
        }
    }

   

}

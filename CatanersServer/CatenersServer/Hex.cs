using CatanersShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatenersServer
{
    class Hex
    {
        public List<Building> buildings;
        public int dice;
        public Resource.TYPE type;
        public Robber robber = null;

        public Hex(Resource.TYPE type)
        {
            this.type = type;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanersShared
{
    public class GamePlayer : Player
    {
        private int victoryPoints;
        private bool isMyTurn;
        public int resourceCount;
        public Dictionary<Resource.TYPE,int> resources;


        public GamePlayer(String Username)
            : base(Username)
        {
            resources = new Dictionary<Resource.TYPE, int>();
            resources.Add(Resource.TYPE.Brick, 0);
            resources.Add(Resource.TYPE.Ore, 0);
            resources.Add(Resource.TYPE.Sheep, 0);
            resources.Add(Resource.TYPE.Wheat, 0);
            resources.Add(Resource.TYPE.Wood, 0);

            resourceCount = resources[Resource.TYPE.Brick] + resources[Resource.TYPE.Ore] + resources[Resource.TYPE.Sheep] + resources[Resource.TYPE.Wheat] + resources[Resource.TYPE.Wood]; 
        }
    }
}

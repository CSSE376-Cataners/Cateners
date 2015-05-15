using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanersShared
{
    public class GamePlayer : Player
    {
        private int victoryPoints;
        private ArrayList settlementList;
        public string color;
        public int resourceCount
        {
            get
            {
                int toReturn = 0;
                foreach (Resource.TYPE t in resources.Keys)
                {
                    toReturn += resources[t];
                }
                return toReturn;
            }
        }
        public Dictionary<Resource.TYPE,int> resources;

        public GamePlayer(String Username)
            : base(Username)
        {
            this.color = "NA";
            resources = new Dictionary<Resource.TYPE, int>();
            resources.Add(Resource.TYPE.Brick, 0);
            resources.Add(Resource.TYPE.Ore, 0);
            resources.Add(Resource.TYPE.Sheep, 0);
            resources.Add(Resource.TYPE.Wheat, 0);
            resources.Add(Resource.TYPE.Wood, 0);
            this.settlementList = new ArrayList();
            //resourceCount = resources[Resource.TYPE.Brick] + resources[Resource.TYPE.Ore] + resources[Resource.TYPE.Sheep] + resources[Resource.TYPE.Wheat] + resources[Resource.TYPE.Wood]; 
        }

        public void addSettlement(int settlementID)
        {
            this.settlementList.Add(settlementID);
        }

        public void setColor(string color)
        {
            this.color = color;
        }

        public string getColor()
        {
            return this.color;
        }
    }
}

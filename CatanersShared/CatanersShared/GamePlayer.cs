﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanersShared
{
    public class GamePlayer : Player
    {
        private int victoryPoints;
        public bool isMyTurn;
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
            resources = new Dictionary<Resource.TYPE, int>();
            resources.Add(Resource.TYPE.Brick, 10);
            resources.Add(Resource.TYPE.Ore, 10);
            resources.Add(Resource.TYPE.Sheep, 10);
            resources.Add(Resource.TYPE.Wheat, 10);
            resources.Add(Resource.TYPE.Wood, 10);

            //resourceCount = resources[Resource.TYPE.Brick] + resources[Resource.TYPE.Ore] + resources[Resource.TYPE.Sheep] + resources[Resource.TYPE.Wheat] + resources[Resource.TYPE.Wood]; 
        }
    }
}

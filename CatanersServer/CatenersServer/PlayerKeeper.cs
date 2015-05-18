using CatanersShared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatenersServer
{
    public class PlayerKeeper
    {
        private string username;
        private ArrayList ownedSettlements;
        private ArrayList ownedRoads;
        private ArrayList ownedPaths;
        private int settleCount;
        private int roadCount;
        private ServerLogic currServerLogic;

        public PlayerKeeper(string username, ServerLogic currServer)
        {
            this.username = username;
            this.ownedRoads = new ArrayList();
            this.ownedSettlements = new ArrayList();
            this.settleCount = 0;
            this.roadCount = 0;
            this.ownedPaths = new ArrayList();
            this.currServerLogic = currServer;
        }

        public int getRoadCount()
        {
            return this.roadCount;
        }

        public int getSettlementCount()
        {
            return this.settleCount;
        }

        public RoadPath addToRoads(int x, int[] neighbors)
        {
            this.ownedRoads.Add(x);
            this.roadCount += 1;
            RoadPath currPath = new RoadPath(x);
            foreach (RoadPath path in this.ownedPaths)
            {
                foreach (int neighbor in neighbors)
                {
                    if (path.getFront() == neighbor)
                    {
                        path.addToFront(x);
                        currPath = path;
                    }
                    else if (path.getBack() == neighbor)
                    {
                        path.addToBack(x);
                        currPath = path;
                    }
                }
            }
            if (currPath.getSize().Equals(1))
            {
                this.ownedPaths.Add(currPath);
            }
            if (Data.INSTANCE.LongestRoadCount < currPath.getSize())
            {
                Data.INSTANCE.UserWithLongestRoad = this.username;
                Data.INSTANCE.LongestRoadCount = currPath.getSize();
            }
            return new RoadPath(x);
        }

        public void addToSettlements(int x)
        {
            this.ownedSettlements.Add(x);
            this.settleCount += 1;
        }
        public ArrayList getRoads()
        {
            return this.ownedRoads;
        }
        public ArrayList getSettlements()
        {
            return this.ownedSettlements;
        }
    }
 
}

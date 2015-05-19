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
            ArrayList toAdd = new ArrayList();
            if (this.ownedPaths.Count > 0)
            {
                foreach (RoadPath path in this.ownedPaths)
                {
                    RoadPath gennedPath = path.generateNewPath(x, neighbors);
                    if (gennedPath.Equals(path))
                    {
                        currPath = gennedPath;
                    }
                    else
                    {
                        currPath = gennedPath;
                        this.ownedPaths.Add(currPath);
                        break;
                    }
                }
            }
            else
            {
                this.ownedPaths.Add(currPath);
            }
            foreach (RoadPath path2 in this.ownedPaths)
            {
                if (!currPath.Equals(path2))
                {
                    if (currPath.getFront() == path2.getFront())
                    {
                        currPath = currPath.joinFrontFront(path2);
                        if (this.currServerLogic.LongestRoadCount < currPath.getSize())
                        {
                            this.currServerLogic.UserWithLongestRoad = this.username;
                            this.currServerLogic.LongestRoadCount = currPath.getSize();
                        }
                        return currPath;
                    }
                    else if (currPath.getFront() == path2.getBack())
                    {
                        currPath = currPath.joinFrontBack(path2);
                        if (this.currServerLogic.LongestRoadCount < currPath.getSize())
                        {
                            this.currServerLogic.UserWithLongestRoad = this.username;
                            this.currServerLogic.LongestRoadCount = currPath.getSize();
                        }
                        return currPath;
                    }
                    else if (currPath.getBack() == path2.getFront())
                    {
                        currPath = currPath.joinBackFront(path2);
                        if (this.currServerLogic.LongestRoadCount < currPath.getSize())
                        {
                            this.currServerLogic.UserWithLongestRoad = this.username;
                            this.currServerLogic.LongestRoadCount = currPath.getSize();
                        }
                        return currPath;
                    }
                    else if (currPath.getBack() == path2.getBack() && currPath.getRoadIDs()[1] != path2.getRoadIDs()[1])
                    {
                        currPath = currPath.joinBackBack(path2);
                        if (this.currServerLogic.LongestRoadCount < currPath.getSize())
                        {
                            this.currServerLogic.UserWithLongestRoad = this.username;
                            this.currServerLogic.LongestRoadCount = currPath.getSize();
                        }
                        return currPath;
                    }
                }
            }
            if (this.currServerLogic.LongestRoadCount < currPath.getSize())
            {
                this.currServerLogic.UserWithLongestRoad = this.username;
                this.currServerLogic.LongestRoadCount = currPath.getSize();
            }
            return currPath;
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

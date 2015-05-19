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
                        toAdd.Add(currPath);
                    }
                }
                this.ownedPaths.AddRange(toAdd);
            }
            else
            {
                this.ownedPaths.Add(currPath);
            }
            foreach(RoadPath path1 in this.ownedPaths)
            {
                RoadPath tempPath = path1;
                foreach (RoadPath path2 in this.ownedPaths)
                {
                    if (!path1.Equals(path2))
                    {
                        if (this.currServerLogic.getRoadList()[(tempPath.getFront())].getNeighbors().Contains(path2.getFront()))
                        {
                            tempPath = tempPath.joinFrontFront(path2.getRoadIDs());
                            if (this.currServerLogic.LongestRoadCount < tempPath.getSize())
                            {
                                this.currServerLogic.UserWithLongestRoad = this.username;
                                this.currServerLogic.LongestRoadCount = tempPath.getSize();
                            }
                            return tempPath;
                        }
                        else if (this.currServerLogic.getRoadList()[(tempPath.getFront())].getNeighbors().Contains(path2.getBack()))
                        {
                            tempPath = tempPath.joinFrontBack(path2.getRoadIDs());
                            if (this.currServerLogic.LongestRoadCount < tempPath.getSize())
                            {
                                this.currServerLogic.UserWithLongestRoad = this.username;
                                this.currServerLogic.LongestRoadCount = tempPath.getSize();
                            }
                            return tempPath;
                        }
                        else if (this.currServerLogic.getRoadList()[(tempPath.getBack())].getNeighbors().Contains(path2.getFront()))
                        {
                            tempPath = tempPath.joinBackFront(path2.getRoadIDs());
                            if (this.currServerLogic.LongestRoadCount < tempPath.getSize())
                            {
                                this.currServerLogic.UserWithLongestRoad = this.username;
                                this.currServerLogic.LongestRoadCount = tempPath.getSize();
                            }
                            return tempPath;
                        }
                        else if (this.currServerLogic.getRoadList()[(tempPath.getBack())].getNeighbors().Contains(path2.getBack()))
                        {
                            tempPath = tempPath.joinBackBack(path2.getRoadIDs());
                            if (this.currServerLogic.LongestRoadCount < tempPath.getSize())
                            {
                                this.currServerLogic.UserWithLongestRoad = this.username;
                                this.currServerLogic.LongestRoadCount = tempPath.getSize();
                            }
                            return tempPath;
                        }
                    }
                }
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

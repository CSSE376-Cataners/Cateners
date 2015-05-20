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
            bool longestRoadChanged = false;
            this.ownedRoads.Add(x);
            this.roadCount += 1;
            RoadPath currPath = new RoadPath(x);
            ArrayList toAdd = new ArrayList();
            if (this.ownedPaths.Count > 0)
            {
                foreach (RoadPath path in this.ownedPaths)
                {
                    RoadPath gennedPath = path.generateNewPath(x, neighbors);
                    if (!gennedPath.Equals(path))
                    {
                        currPath = gennedPath;
                        toAdd.Add(currPath);
                        break;
                    }
                    else
                    {
                        if (gennedPath.wasChanged)
                        {
                            currPath = gennedPath;
                            gennedPath.wasChanged = false;
                            break;
                        }
                    }
                }
            }
            else
            {
                this.ownedPaths.Add(currPath);
            }
            this.ownedPaths.AddRange(toAdd);
            ArrayList toAdd2 = new ArrayList();
            foreach (RoadPath tempPath in this.ownedPaths)
            {
                foreach (RoadPath path2 in this.ownedPaths)
                {
                    if (!tempPath.Equals(path2) && ((tempPath.getSize() < 2) || (path2.getSize() < 2) || tempPath.getRoadIDs()[tempPath.getRoadIDs().Length - 2] != path2.getRoadIDs()[path2.getRoadIDs().Length - 2]))
                    {
                        if (this.currServerLogic.getRoadList()[tempPath.getFront()].getNeighbors().Contains(path2.getFront()))
                        {
                            RoadPath newPath = new RoadPath(tempPath.joinFrontFront(path2));
                            if (this.currServerLogic.LongestRoadCount < newPath.getSize())
                            {
                                this.currServerLogic.UserWithLongestRoad = this.username;
                                this.currServerLogic.LongestRoadCount = newPath.getSize();
                                toAdd2.Add(newPath);
                                longestRoadChanged = true;
                            }
                        }
                        else if (this.currServerLogic.getRoadList()[tempPath.getFront()].getNeighbors().Contains(path2.getBack()))
                        {
                            RoadPath newPath = new RoadPath(tempPath.joinFrontBack(path2));
                            if (this.currServerLogic.LongestRoadCount < newPath.getSize())
                            {
                                this.currServerLogic.UserWithLongestRoad = this.username;
                                this.currServerLogic.LongestRoadCount = newPath.getSize();
                                toAdd2.Add(newPath);
                                longestRoadChanged = true;
                            }
                        }
                        else if (this.currServerLogic.getRoadList()[tempPath.getBack()].getNeighbors().Contains(path2.getFront()))
                        {
                            RoadPath newPath = new RoadPath(tempPath.joinBackFront(path2));
                            if (this.currServerLogic.LongestRoadCount < newPath.getSize())
                            {
                                this.currServerLogic.UserWithLongestRoad = this.username;
                                this.currServerLogic.LongestRoadCount = newPath.getSize();
                                toAdd2.Add(newPath);
                                longestRoadChanged = true;
                            }
                        }
                        else if (this.currServerLogic.getRoadList()[tempPath.getBack()].getNeighbors().Contains(path2.getBack()) && tempPath.getRoadIDs()[1] != path2.getRoadIDs()[1])
                        {
                            RoadPath newPath = new RoadPath(tempPath.joinBackBack(path2));
                            if (this.currServerLogic.LongestRoadCount < newPath.getSize())
                            {
                                this.currServerLogic.UserWithLongestRoad = this.username;
                                this.currServerLogic.LongestRoadCount = newPath.getSize();
                                toAdd2.Add(newPath);
                                longestRoadChanged = true;
                            }
                        }
                    }
                }
            }
            if (this.currServerLogic.LongestRoadCount < currPath.getSize())
            {
                this.currServerLogic.UserWithLongestRoad = this.username;
                this.currServerLogic.LongestRoadCount = currPath.getSize();
                longestRoadChanged = true;
            }
            this.ownedPaths.AddRange(toAdd2);
            if (longestRoadChanged)
            {
                ServerPlayer player = (ServerPlayer)this.currServerLogic.getLobby().Players[0];
                player.client.sendToLobby(new Message(new PopUpMessage("There's a New Longest Road!", "The player with the new Longest Road is: " + this.username, PopUpMessage.TYPE.Notification).toJson(), Translation.TYPE.PopUpMessage).toJson());
            }
            return currPath;
        }

        //this.currServerLogic.getRoadList()[currPath.getFront()].getNeighbors().Contains(path2.getBack())

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

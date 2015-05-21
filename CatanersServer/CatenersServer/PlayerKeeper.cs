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

        public ArrayList getRoadPaths()
        {
            return this.ownedPaths;
        }

        public void addToRoads(int x, int[] neighbors)
        {
            bool longestRoadChanged = false;
            this.ownedRoads.Add(x);
            this.roadCount += 1;
            RoadPath tempPath = new RoadPath(x);
            RoadPath newPath = tempPath;
            this.ownedPaths.Add(tempPath);
            ArrayList toAdd = new ArrayList();
            for (int p = 0; p < this.ownedPaths.Count; p++ )
            {
                RoadPath path2 = (RoadPath)this.ownedPaths[p];
                if (!tempPath.Equals(path2))
                {
                    RoadServer[] allRoads = this.currServerLogic.getRoadList();
                    int[] path2IDs = path2.getRoadIDs();
                    int path2Length = path2IDs.Length;
                    int path2Front = path2IDs[path2Length - 1];
                    int[] p2FNeighbors = allRoads[path2Front].getNeighbors();
                    int path2Back = path2IDs[0];
                    int[] p2BNeighbors = allRoads[path2Back].getNeighbors();
                    int[] tempPathIDs = tempPath.getRoadIDs();
                    int tempPathLength = tempPathIDs.Length;
                    int tempPathFront = tempPathIDs[tempPathLength - 1];
                    int[] tPFNeighbors = allRoads[tempPathFront].getNeighbors();
                    int tempPathBack = tempPathIDs[0];
                    int[] tPBNeighbors = allRoads[tempPathBack].getNeighbors();
                    if (p2FNeighbors.Contains(tempPathBack) && this.checkTheNeighbors(tPBNeighbors, p2FNeighbors, path2Front, tempPathFront, path2IDs))
                    {
                        newPath = new RoadPath(path2.joinFrontBack(tempPath));
                        toAdd.Add(newPath);
                        if (this.currServerLogic.LongestRoadCount < newPath.getRoadIDs().Length)
                        {
                            this.currServerLogic.UserWithLongestRoad = this.username;
                            this.currServerLogic.LongestRoadCount = newPath.getRoadIDs().Length;
                            tempPath.sameLength = false;
                            longestRoadChanged = true;
                        }
                    }
                }
            }
            ArrayList toAdd2 = new ArrayList();
            if (toAdd.Count > 0)
            {
                for (int k = 0; k < toAdd.Count; k++)
                {
                   RoadPath finPath1 = (RoadPath) toAdd[k];
                    for (int z = 0; z < toAdd.Count; z++)
                    {
                        RoadPath finPath2 = (RoadPath)toAdd[z];
                        if (finPath1!=finPath2 && !this.currServerLogic.getRoadList()[finPath1.getRoadIDs()[finPath1.getRoadIDs().Length - 2]].getNeighbors().Contains(finPath2.getRoadIDs()[finPath2.getRoadIDs().Length - 2]))
                        {
                            RoadPath newPath2 = new RoadPath(finPath1.merge(finPath2));
                            toAdd2.Add(newPath2);
                            if (this.currServerLogic.LongestRoadCount < newPath2.getRoadIDs().Length)
                            {
                                this.currServerLogic.UserWithLongestRoad = this.username;
                                this.currServerLogic.LongestRoadCount = newPath2.getRoadIDs().Length;
                                tempPath.sameLength = false;
                                longestRoadChanged = true;
                            }
                        }
                    }
                }
            }
            this.ownedPaths.AddRange(toAdd);
            this.ownedPaths.AddRange(toAdd2);
            if (longestRoadChanged)
            {
                ServerPlayer player = (ServerPlayer)this.currServerLogic.getLobby().Players[0];
                player.client.sendToLobby(new Message(new PopUpMessage("There's a New Longest Road!", "The player with the new Longest Road is: " + this.username, PopUpMessage.TYPE.Notification).toJson(), Translation.TYPE.PopUpMessage).toJson());
            }
        }

        public bool checkTheNeighbors(int[] neighbors1, int[] neighbors2, int node1, int node2, int[] backHalf)
        {
            int shared = -1;
            foreach (int N1 in neighbors1)
            {
                if (neighbors2.Contains(N1))
                {
                    shared = N1;
                }
            }
            if (backHalf.Contains(shared))
            {
                return false;
            }
            return true;
        }

        //this.currServerLogic.getRoadList()[currPath.getFront()].getNeighbors().Contains(path2.getRoadIDs()[path2.getRoadIDs().Length - 1])

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

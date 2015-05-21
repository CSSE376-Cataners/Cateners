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
        private ArrayList ownedSettlements;
        private ArrayList ownedRoads;
        private ArrayList ownedPaths;
        private int settleCount;
        private int roadCount;
        private ServerLogic currServerLogic;
        public List<int> maxList;
        public GamePlayer player;

        public PlayerKeeper(GamePlayer player, ServerLogic currServer)
        {
            this.player = player;
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
            this.ownedRoads.Add(x);
            List<int> max = new List<int>();
            foreach (int z in this.ownedRoads)
            {
                List<int> newList = new List<int>();
                List<int> tempPath = findLongestPath(newList, z);
                if (tempPath.Count > max.Count)
                {
                    max = tempPath;
                }
            }
            if (this.currServerLogic.LongestRoadCount < max.Count)
            {
                this.currServerLogic.LongestRoadCount = max.Count;
                this.currServerLogic.UserWithLongestRoad.victoryPoints -= 2;
                this.currServerLogic.UserWithLongestRoad = this.player;
                this.player.victoryPoints += 2;
                ServerPlayer player = (ServerPlayer)this.currServerLogic.getLobby().Players[0];
<<<<<<< HEAD
                player.client.sendToLobby(new Message(new PopUpMessage("There's a New Longest Road!", "The player with the new Longest Road is: " + this.player.Username, PopUpMessage.TYPE.Notification).toJson(), Translation.TYPE.PopUpMessage).toJson());
                this.currServerLogic.checkWinCondition(this.player);
=======
                player.client.sendToLobby(new Message(new PopUpMessage("There's a New Longest Road!", "The player with the new Longest Road is: " + this.username, PopUpMessage.TYPE.Notification).toJson(), Translation.TYPE.PopUpMessage).toJson());
                if (this.currServerLogic.checkWinCondition(this.player))
                {
                    PopUpMessage popup = new PopUpMessage("WIN!", player.Username + " has won the game with " + this.player.victoryPoints + " Victory Points", PopUpMessage.TYPE.Notification);
                    player.client.sendToLobby(new Message(popup.toJson(), Translation.TYPE.PopUpMessage).toJson());
                    player.client.leaveLobby();
                };
>>>>>>> origin/master
            }
            this.maxList = max;
        }

        public List<int> findLongestPath(List<int> inputArray, int x)
        {
            if (inputArray.Contains(x) || !this.ownedRoads.Contains(x))
            {
                return inputArray;
            }

            int lastRoad = -1;
            if (inputArray.Count > 1)
            {
                lastRoad = inputArray[inputArray.Count - 1];
            }
            List<int> max = new List<int>();
            foreach (int settle in this.currServerLogic.roadSettlementDict[x])
            {
                if (!this.currServerLogic.settlementRoadDict[settle].Contains(lastRoad))
                {
                    foreach (int z in this.currServerLogic.settlementRoadDict[settle])
                    {
                        List<int> newList = new List<int>(inputArray);
                        newList.Add(x);
                        List<int> tempPath = findLongestPath(newList, z);
                        if (tempPath.Count > max.Count)
                        {
                            max = tempPath;
                        }
                    }
                }
            }
            
            return max;
        }

        public RoadPath looper(RoadPath initialTempPath)
        {
            RoadPath tempPath = initialTempPath;
            RoadPath potentialPath = initialTempPath;
            for (int p = 0; p < this.ownedPaths.Count; p++)
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
                    if (p2FNeighbors.Contains(tempPathBack) && this.checkTheNeighbors(tPBNeighbors, p2FNeighbors, path2Front, tempPathBack, path2IDs))
                    {
                        int[] temporary = path2.joinFrontBack(tempPath);
                        if (temporary.Length > potentialPath.getRoadIDs().Length)
                        {
                            potentialPath = new RoadPath(temporary);
                        }
                    }
                }
            }
            return potentialPath;
        }

        public RoadPath looper2(RoadPath initialTempPath)
        {
            RoadPath tempPath = initialTempPath;
            RoadPath potentialPath = initialTempPath;
            for (int p = 0; p < this.ownedPaths.Count; p++)
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
                    if (p2BNeighbors.Contains(tempPathFront) && this.checkTheNeighbors(tPFNeighbors, p2BNeighbors, path2Back, tempPathFront, path2IDs))
                    {
                        int[] temporary = path2.joinBackFront(tempPath);
                        if (temporary.Length > potentialPath.getRoadIDs().Length)
                        {
                            potentialPath = new RoadPath(temporary);
                        }
                    }
                }
            }
            return potentialPath;
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

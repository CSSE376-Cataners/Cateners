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
                ServerPlayer sendingPlayer = (ServerPlayer) this.currServerLogic.getLobby().Players[0];
                PopUpMessage longestPopup = new PopUpMessage("There's a NEW LONGEST ROOOOOOOOOOOOOOOOOOAD", player.Username + " has the new longest road with " + max.Count + " roads in it!", PopUpMessage.TYPE.Notification);
                sendingPlayer.client.sendToLobby(new Message(longestPopup.toJson(), Translation.TYPE.PopUpMessage).toJson());
                if (this.currServerLogic.checkWinCondition(this.player))
                {
                    PopUpMessage popup = new PopUpMessage("WIN!", player.Username + " has won the game with " + this.player.victoryPoints + " Victory Points", PopUpMessage.TYPE.Notification);
                    sendingPlayer.client.sendToLobby(new Message(popup.toJson(), Translation.TYPE.PopUpMessage).toJson());
                    sendingPlayer.client.leaveLobby();
                };
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

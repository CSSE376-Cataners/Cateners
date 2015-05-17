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

        public ArrayList addToRoads(int x, int[] neighbors)
        {
            ArrayList cumulativeList = new ArrayList();
            cumulativeList.Add(x);
            ArrayList toRemove = new ArrayList();
            for (int k = 0; k < neighbors.Length; k++)
            {
                foreach (ArrayList path in this.ownedPaths)
                {
                    if (path.Contains(neighbors[k]))
                    {
                        cumulativeList.AddRange(path);
                        toRemove.Add(path);
                    }
                }
            }
            foreach (ArrayList path in toRemove)
            {
                this.ownedPaths.Remove(path);
            }
            this.ownedPaths.Add(cumulativeList);
            this.ownedRoads.Add(x);
            this.roadCount += 1;
            if (cumulativeList.Count > Data.INSTANCE.LongestRoadCount)
            {
                Data.INSTANCE.LongestRoadCount = cumulativeList.Count;
                Data.INSTANCE.UserWithLongestRoad = this.username;
                ServerPlayer player = (ServerPlayer)this.currServerLogic.getLobby().Players[0];
                player.client.sendToLobby(new Message(new PopUpMessage("There's a New Longest Road!", "The player with the new Longest Road is: " + this.username, PopUpMessage.TYPE.Notification).toJson(), Translation.TYPE.PopUpMessage).toJson());
                return cumulativeList;
            }
            return cumulativeList;
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

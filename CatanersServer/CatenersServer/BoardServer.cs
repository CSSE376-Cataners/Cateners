using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatenersServer
{
    public class HexServer
    {
        private int typeNum;
        private int placementNumber;
        private int rollNumber;
        private SettlementServer[] settlementArray;
        private RoadServer[] roadArray;

        public HexServer(int typeNum)
        {
            this.typeNum = typeNum;
            this.placementNumber = 0;
            this.rollNumber = 0;
            this.settlementArray = new SettlementServer[6];
        }

        public SettlementServer[] getSettlementArray()
        {
            return this.settlementArray;
        }

        public virtual void setPlacementNumber(int num)
        {
            this.placementNumber = num;
        }

        public virtual int getPlacementNumber()
        {
            return this.placementNumber;
        }

        public int getHexType()
        {
            return this.typeNum;
        }

        public int getRollNumber()
        {
            return rollNumber;
        }

        public void setRoadArray(RoadServer[] roadArray)
        {
            this.roadArray = roadArray;
        }

        public void setRollNumber(int rollNum)
        {
            this.rollNumber = rollNum;
        }

        public void setSettlementArray(SettlementServer[] newArray)
        {
            this.settlementArray = newArray;
        }

        public int[] toShadow()
        {
            int[] toReturn = new int[15];
            toReturn[0] = this.typeNum;
            toReturn[1] = this.placementNumber;
            toReturn[2] = this.rollNumber;
            for (int i = 0; i < 6; i++)
            {
                toReturn[i + 3] = this.settlementArray[i].getPlacementNumber();
            }
            for (int k = 0; k < 6; k++)
            {
                toReturn[k + 9] = this.roadArray[k].getPlacementNumber();
            }
            return toReturn;
        }
    }

    public class RoadServer
    {
        private int placementNumber;
        public Boolean canAddComponent;
        private Boolean isActive;
        private int[] neighbors;
        private int[] settlements;

        public RoadServer(int placementNumber)
        {
            this.placementNumber = placementNumber;
            this.canAddComponent = true;
            this.isActive = false;
        }

        public int getPlacementNumber()
        {
            return this.placementNumber;
        }

        public void setActive()
        {
            this.isActive = !this.isActive;
        }

        public Boolean getIsActive()
        {
            return this.isActive;
        }

        public void setNeighbors(int[] neighbors)
        {
            this.neighbors = neighbors;
        }

        public int[] getNeighbors()
        {
            return this.neighbors;
        }

        public void setSettlements(int[] p)
        {
            this.settlements = p;
        }

        public int[] getSettlements()
        {
            return this.settlements;
        }
    }

    public class SettlementServer
    {
        private int typeNum;
        private int placementNumber;
        public Boolean canAddComponent;
        private Boolean isActive;
        private int[] neighbors;
        private int[] roadNeighbors;

        public SettlementServer(int typeNum, int placementNumber)
        {
            this.typeNum = typeNum;
            this.placementNumber = placementNumber;
            this.canAddComponent = true;
            this.isActive = false;
        }

        public int getPlacementNumber()
        {
            return this.placementNumber;
        }

        public int getTypeNum()
        {
            return this.typeNum;
        }

        public void setActive()
        {
            this.isActive = !this.isActive;
        }

        public Boolean getIsActive()
        {
            return this.isActive;
        }

        public void setNeighbors(int[] neighbors)
        {
            this.neighbors = neighbors;
        }

        public int[] getNeighbors()
        {
            return this.neighbors;
        }

        public void setRoads(int[] p)
        {
            this.roadNeighbors = p;
        }

        public int[] getRoads()
        {
            return this.roadNeighbors;
        }
    }
}

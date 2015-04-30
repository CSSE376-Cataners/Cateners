using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveEngine;
using WaveEngine.Components.Graphics2D;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Managers;

namespace WaveEngineGameProject
{
    public class SettlementHolder
    {
        private Entity settlement;
        private int placementNumber;
        public Boolean canAddComponent;
        public SettlementHolder(Entity settlement, int placementNumber)
        {
            this.settlement = settlement;
            this.placementNumber = placementNumber;
            this.canAddComponent = true;
        }
        public virtual int getPlacementNumber()
        {
            return this.placementNumber;
        }
        public Entity getSettlement()
        {
            return this.settlement;
        }
    }

    public class HexHolder
    {
        private Entity hex;
        private int placementNumber;
        private int rollNumber;
        private Entity rollEntity;
        private SettlementHolder[] settlementList;
        private int type;

        public HexHolder(Entity hex, int type)
        {
            this.hex = hex;
            this.type = type;
            this.placementNumber = 0;
            this.rollNumber = 0;
            this.settlementList = new SettlementHolder[6];
        }

        public SettlementHolder[] getSettlementList()
        {
            return this.settlementList;
        }

        public void setPlacementNumber(int num)
        {
            this.placementNumber = num;
        }

        public virtual int getPlacementNumber()
        {
            return this.placementNumber;
        }

        public Entity getHex()
        {
            return this.hex;
        }

        public int getRollNumber()
        {
            return rollNumber;
        }

        public void setRollNumber(int rollNum)
        {
            this.rollNumber = rollNum;
        }

        public void setRollEntity(Entity rollEnt)
        {
            this.rollEntity = rollEnt;
        }
        public Entity getRollEntity()
        {
            return this.rollEntity;
        }

        public void setSettlementList(SettlementHolder[] newArray)
        {
            this.settlementList = newArray;
        }
        public int[] toShadow()
        {
            return new int[] { this.type, this.placementNumber, this.rollNumber, this.settlementList[0].getPlacementNumber(), this.settlementList[1].getPlacementNumber(), this.settlementList[2].getPlacementNumber(), this.settlementList[3].getPlacementNumber(), this.settlementList[4].getPlacementNumber(), this.settlementList[5].getPlacementNumber() };
        }
    }

    public class LocalConversion
    {

    }
}
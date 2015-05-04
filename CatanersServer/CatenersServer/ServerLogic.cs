using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using WaveEngine;
using WaveEngine.Framework;
using WaveEngine.Components.Graphics2D;
using WaveEngine.Framework.Graphics;
using CatanersShared;

namespace CatenersServer
{
    public class SettlementServer
    {
        private int typeNum;
        private int placementNumber;
        public Boolean canAddComponent;
        public SettlementServer(int typeNum, int placementNumber)
        {
            this.typeNum = typeNum;
            this.placementNumber = placementNumber;
            this.canAddComponent = true;
        }

        public int getPlacementNumber()
        {
            return this.placementNumber;
        }

        public int getTypeNum()
        {
            return this.typeNum;
        }
    }

    public class HexServer
    {
        private int typeNum;
        private int placementNumber;
        private int rollNumber;
        private SettlementServer[] settlementArray;

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
            return new int[] { this.typeNum, this.placementNumber, this.rollNumber, this.settlementArray[0].getPlacementNumber(), this.settlementArray[1].getPlacementNumber(), this.settlementArray[2].getPlacementNumber(), this.settlementArray[3].getPlacementNumber(), this.settlementArray[4].getPlacementNumber(), this.settlementArray[5].getPlacementNumber() };
        }
    }
    public class ServerLogic
    {
        private static int numberOfHexes = 19;
        private HexServer[] hexArray;
        private SettlementServer[] settlementArray;
        private Lobby lobby;
        public int playerTurn;
        public GameLobby gameLobby;
        public int dice;

        public ServerLogic(Lobby lobby)
        {
            this.hexArray = new HexServer[numberOfHexes];
            this.settlementArray = new SettlementServer[54];
            this.generatehexArray();
            this.generateDefaultSettlements();
            this.assignSettlements();
            this.lobby = lobby;
            gameLobby = new GameLobby(lobby);
        }

        public SettlementServer[] getSettlementList()
        {
            return this.settlementArray;
        }

        public string sendGeneration()
        {
            this.generatehexArray();
            this.generateDefaultSettlements();
            this.assignSettlements();
            int[][] passedArray = new int[this.hexArray.Length][];
            for (int k = 0; k < this.hexArray.Length; k++)
            {
                passedArray[k] = this.hexArray[k].toShadow();
            }
            return Translation.intArraytwotoJson(passedArray);
        }

        public Boolean determineSettlementAvailability()
        {
            return false;
        }

        public void generatehexArray()
        {
            System.Random r = new System.Random();
            ArrayList rangeList = new ArrayList();
            int count = 0;
            for (int i = 0; i < 4; i++)
            {
                this.hexArray[count] = new HexServer(1);
                count++;
            }

            this.hexArray[count] = new HexServer(2);
            count++;

            for (int k = 0; k < 3; k++)
            {
                this.hexArray[count] = new HexServer(3);
                count++;
            }

            for (int k = 0; k < 3; k++)
            {
                this.hexArray[count] = new HexServer(4);
                count++;
            }

            for (int p = 0; p < 4; p++)
            {
                this.hexArray[count] = new HexServer(5);
                count++;
            }

            for (int u = 0; u < 4; u++)
            {
                this.hexArray[count] = new HexServer(6);
                count++;
            }

            rangeList.AddRange(new Object[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 });
            for (int g = 0; g < 19; g++)
            {
                int rInt = r.Next(0, rangeList.Count);
                int nextIndex = (int)rangeList[rInt];
                rangeList.RemoveAt(rInt);
                this.hexArray[g].setPlacementNumber(nextIndex);
            }
            this.assignRollNumbers();
            this.generateDefaultSettlements();
            this.assignSettlements();
        }

        public void generateDefaultSettlements()
        {
            this.settlementArray = new SettlementServer[54];
            for (int i = 0; i < 54; i++)
            {
                this.settlementArray[i] = new SettlementServer(1, i);
            }
        }

        public void assignSettlements()
        {
            for (int i = 0; i < this.hexArray.Length; i++)
            {
                HexServer currHex = this.hexArray[i];
                int currNum = currHex.getPlacementNumber();
                if (currNum < 3)
                {
                    int a = currNum;
                    int b = a + 3;
                    int c = b + 4;
                    int d = c + 5;
                    SettlementServer[] newArray = new SettlementServer[6] { this.settlementArray[a], this.settlementArray[b], this.settlementArray[b + 1], this.settlementArray[c], this.settlementArray[c + 1], this.settlementArray[d] };
                    hexArray[i].setSettlementArray(newArray);
                }
                else if (currNum < 7)
                {
                    int a = currNum + 4;
                    int b = a + 4;
                    int c = b + 5;
                    int d = c + 6;
                    SettlementServer[] newArray = new SettlementServer[6] { this.settlementArray[a], this.settlementArray[b], this.settlementArray[b + 1], this.settlementArray[c], this.settlementArray[c + 1], this.settlementArray[d] };
                    hexArray[i].setSettlementArray(newArray);
                }
                else if (currNum < 12)
                {
                    int a = currNum + 9;
                    int b = a + 5;
                    int c = b + 6;
                    int d = c + 6;
                    SettlementServer[] newArray = new SettlementServer[6] { this.settlementArray[a], this.settlementArray[b], this.settlementArray[b + 1], this.settlementArray[c], this.settlementArray[c + 1], this.settlementArray[d] };
                    hexArray[i].setSettlementArray(newArray);
                }
                else if (currHex.getPlacementNumber() < 16)
                {
                    int a = currNum + 16;
                    int b = a + 5;
                    int c = b + 5;
                    int d = c + 5;
                    SettlementServer[] newArray = new SettlementServer[6] { this.settlementArray[a], this.settlementArray[b], this.settlementArray[b + 1], this.settlementArray[c], this.settlementArray[c + 1], this.settlementArray[d] };
                    hexArray[i].setSettlementArray(newArray);
                }
                else
                {
                    int a = currNum + 23;
                    int b = a + 4;
                    int c = b + 4;
                    int d = c + 4;
                    SettlementServer[] newArray = new SettlementServer[6] { this.settlementArray[a], this.settlementArray[b], this.settlementArray[b + 1], this.settlementArray[c], this.settlementArray[c + 1], this.settlementArray[d] };
                    hexArray[i].setSettlementArray(newArray);
                }
            }
        }

        public int[][] gethexArray()
        {
            int[][] array = new int[this.hexArray.Length][];
            for (int i = 0; i < this.hexArray.Length; i++)
            {
                array[i] = this.hexArray[i].toShadow();
            }
            return array;
        }

        public void assignRollNumbers()
        {
            System.Random r = new System.Random();
            ArrayList rollList = new ArrayList();
            rollList.AddRange(new int[] { 2, 3, 3, 4, 4, 5, 5, 6, 6, 8, 8, 9, 9, 10, 10, 11, 11, 12 });
            for (int k = 0; k < 19; k++)
            {
                if (hexArray[k].getHexType() != 4)
                {
                    int rInt = r.Next(0, rollList.Count);
                    int nextIndex = (int)rollList[rInt];
                    rollList.RemoveAt(rInt);
                    this.hexArray[k].setRollNumber(nextIndex);
                }
            }
        }

        public void generateRandomDiceRoll()
        {
            Random rand = new Random();
            this.dice = rand.Next(1, 7) + rand.Next(1,7);
            
        }

        public void updateTurn()
        {
            switch (playerTurn)
            {
                case 0:
                    playerTurn = 1;
                    break;
                case 1:
                    playerTurn = 2;
                    break;
                case 2:
                    playerTurn = 3;
                    break;
                case 3:
                    playerTurn = 0;
                    break;
            }
        }

        public string getPlayerResources(GamePlayer player)
        {
            return player.resources.ToString();
        }

        public void ResourceAllocationAfterDiceRoll()
        {
            switch (dice)
            {
                case 2:
                    //give players who own spot with 2 resources
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 10:
                    break;
                case 11:
                    break;
                case 12:
                    break;

            }
        }
    }

}

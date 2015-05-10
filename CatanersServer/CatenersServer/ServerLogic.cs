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
    public class RoadServer
    {
        private int placementNumber;
        public Boolean canAddComponent;
        private Boolean isActive;
        private int[] neighbors;

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
    }

    public class SettlementServer
    {
        private int typeNum;
        private int placementNumber;
        public Boolean canAddComponent;
        private Boolean isActive;
        private int[] neighbors;

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
    }

    public class HexServer
    {
        private int typeNum;
        private int placementNumber;
        private int rollNumber;
        private SettlementServer[] settlementArray;
        private int[] roadArray;

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

        public void setRoadArray(int[] roadArray)
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
        public Trade onGoingTrade;
        public int dice;
        private RoadServer[] roadArray;
        private Dictionary<int, int[]> neighborDict = new Dictionary<int, int[]>();
        private Dictionary<int, int[]> roadDict = new Dictionary<int, int[]>();
        public ServerLogic(Lobby lobby)
        {
            this.hexArray = new HexServer[numberOfHexes];
            this.settlementArray = new SettlementServer[54];
            #region
            neighborDict.Add(0, new int[] { 3, 4 });
            neighborDict.Add(1, new int[] { 4, 5 });
            neighborDict.Add(2, new int[] { 5, 6 });
            neighborDict.Add(3, new int[] { 0, 7 });
            neighborDict.Add(4, new int[] { 0, 8, 1 });
            neighborDict.Add(5, new int[] { 1, 9, 2 });
            neighborDict.Add(6, new int[] { 2, 10 });
            neighborDict.Add(7, new int[] { 3, 11, 12 });
            neighborDict.Add(8, new int[] { 4, 12, 13 });
            neighborDict.Add(9, new int[] { 5, 13, 14 });
            neighborDict.Add(10, new int[] { 6, 14, 15 });
            neighborDict.Add(11, new int[] { 7, 16 });
            neighborDict.Add(12, new int[] { 7, 17, 8 });
            neighborDict.Add(13, new int[] { 8, 18, 9});
            neighborDict.Add(14, new int[] { 9, 19, 10 });
            neighborDict.Add(15, new int[] { 10, 20 });
            neighborDict.Add(16, new int[] { 11, 21, 22 });
            neighborDict.Add(17, new int[] { 12, 22, 23 });
            neighborDict.Add(18, new int[] { 13, 23, 24 });
            neighborDict.Add(19, new int[] { 14, 24, 25 });
            neighborDict.Add(20, new int[] { 15, 25, 26 });
            neighborDict.Add(21, new int[] { 16, 27 });
            neighborDict.Add(22, new int[] { 16, 28, 17 });
            neighborDict.Add(23, new int[] { 17, 29, 18 });
            neighborDict.Add(24, new int[] { 18, 30, 19 });
            neighborDict.Add(25, new int[] { 19, 31, 20 });
            neighborDict.Add(26, new int[] { 20, 32 });
            neighborDict.Add(27, new int[] { 21, 33 });
            neighborDict.Add(28, new int[] { 22, 33, 34 });
            neighborDict.Add(29, new int[] { 23, 34, 35 });
            neighborDict.Add(30, new int[] { 24, 35, 36 });
            neighborDict.Add(31, new int[] { 25, 36, 37 });
            neighborDict.Add(32, new int[] { 26, 37 });
            neighborDict.Add(33, new int[] { 27, 28, 38 });
            neighborDict.Add(34, new int[] { 28, 29, 39});
            neighborDict.Add(35, new int[] { 29, 30, 40 });
            neighborDict.Add(36, new int[] { 30, 31, 41 });
            neighborDict.Add(37, new int[] { 31, 32, 42 });
            neighborDict.Add(38, new int[] { 33, 43 });
            neighborDict.Add(39, new int[] { 34, 43, 44});
            neighborDict.Add(40, new int[] { 35, 44, 45 });
            neighborDict.Add(41, new int[] { 36, 45, 46 });
            neighborDict.Add(42, new int[] { 37, 46 });
            neighborDict.Add(43, new int[] { 38, 39, 47 });
            neighborDict.Add(44, new int[] { 39, 40, 48 });
            neighborDict.Add(45, new int[] { 40, 41, 49 });
            neighborDict.Add(46, new int[] { 41, 42, 50 });
            neighborDict.Add(47, new int[] { 43, 51 });
            neighborDict.Add(48, new int[] { 44, 51, 52 });
            neighborDict.Add(49, new int[] { 45, 52, 53 });
            neighborDict.Add(50, new int[] { 46, 53 });
            neighborDict.Add(51, new int[] { 47, 48});
            neighborDict.Add(52, new int[] { 48, 49 });
            neighborDict.Add(53, new int[] { 49, 50 });
            #endregion
            #region
            roadDict.Add(0, new int[] { 0, 1, 6, 7, 11, 12});
            roadDict.Add(1, new int[] { 2, 3, 7, 8, 13, 14});
            roadDict.Add(2, new int[] { 4, 5, 8, 9, 15, 16});
            roadDict.Add(3, new int[] { 10, 11, 18, 19, 24, 25});
            roadDict.Add(4, new int[] { 12, 13, 19, 20, 26, 27});
            roadDict.Add(5, new int[] { 14, 15, 20, 21, 28, 29});
            roadDict.Add(6, new int[] { 16, 17, 21, 22, 30, 31});
            roadDict.Add(7, new int[] { 23, 24, 33, 34, 39, 40 });
            roadDict.Add(8, new int[] { 25, 26, 34, 35, 41, 42 });
            roadDict.Add(9, new int[] { 27, 28, 35, 36, 43, 44 });
            roadDict.Add(10, new int[] { 29, 30, 36, 37, 45, 46 });
            roadDict.Add(11, new int[] { 31, 32, 37, 38, 47, 48});
            roadDict.Add(12, new int[] { 40, 41, 49, 50, 54, 55 });
            roadDict.Add(13, new int[] { 42, 43, 50, 51, 56, 57 });
            roadDict.Add(14, new int[] { 44, 45, 51, 52, 58, 59});
            roadDict.Add(15, new int[] { 46, 47, 52, 53, 60, 61});
            roadDict.Add(16, new int[] { 55, 56, 62, 63, 66, 67});
            roadDict.Add(17, new int[] { 57, 58, 63, 64, 68, 69});
            roadDict.Add(18, new int[] { 59, 60, 64, 65, 70, 71 });
            #endregion
            #region
            neighborDict.Add(0, new int[] { 1, 6 });
            neighborDict.Add(1, new int[] {0, 2, 7});
            neighborDict.Add(2, new int[] { 1, 3, 7});
            neighborDict.Add(3, new int[] { 2, 4, 8});
            neighborDict.Add(4, new int[] { 3, 5, 8});
            neighborDict.Add(5, new int[] { 4, 9});
            neighborDict.Add(6, new int[] { 0, 10, 11});
            neighborDict.Add(7, new int[] { 1, 2, 12, 13});
            neighborDict.Add(8, new int[] { 3, 4, 14, 15});
            neighborDict.Add(9, new int[] {5, 16, 17 });
            neighborDict.Add(10, new int[] { 6, 11, 18});
            neighborDict.Add(11, new int[] { 6, 10, 12, 19});
            neighborDict.Add(12, new int[] { 7, 11, 13, 19});
            neighborDict.Add(13, new int[] { 7, 12, 14, 20});
            neighborDict.Add(14, new int[] { 8, 13, 15, 20});
            neighborDict.Add(15, new int[] { 8, 14, 16, 21});
            neighborDict.Add(16, new int[] { 9, 15, 17, 21});
            neighborDict.Add(17, new int[] { 9, 16, 22 });
            neighborDict.Add(18, new int[] { 10, 23, 24});
            neighborDict.Add(19, new int[] { 11, 12, 25, 26 });
            neighborDict.Add(20, new int[] { 13, 14, 27, 28 });
            neighborDict.Add(21, new int[] { 15, 16, 29, 30 });
            neighborDict.Add(22, new int[] { 17, 31, 32});
            neighborDict.Add(23, new int[] { 18, 24, 33});
            neighborDict.Add(24, new int[] { 18, 23, 25, 34});
            neighborDict.Add(25, new int[] { 19, 24, 26, 34});
            neighborDict.Add(26, new int[] { 19, 25, 27, 35});
            neighborDict.Add(27, new int[] { 20, 26, 28, 35});
            neighborDict.Add(28, new int[] { 20, 27, 29, 36});
            neighborDict.Add(29, new int[] { 21, 29, 31, 37});
            neighborDict.Add(30, new int[] { 22, 30, 32, 37 });
            neighborDict.Add(31, new int[] { 22, 30, 32, 37});
            neighborDict.Add(32, new int[] { 22, 31, 28});
            neighborDict.Add(33, new int[] { 23, 39 });
            neighborDict.Add(34, new int[] { 24, 25, 40, 41});
            neighborDict.Add(35, new int[] { 26, 27, 42, 43 });
            neighborDict.Add(36, new int[] { 28, 29, 44, 45 });
            neighborDict.Add(37, new int[] { 30, 31, 46, 47});
            neighborDict.Add(38, new int[] { 32, 48 });
            neighborDict.Add(39, new int[] { 33, 40, 49});
            neighborDict.Add(40, new int[] { 34, 39, 41, 49});
            neighborDict.Add(41, new int[] { 34, 40, 42, 50 });
            neighborDict.Add(42, new int[] {35, 41, 43, 50});
            neighborDict.Add(43, new int[] { 35, 42, 44, 51 });
            neighborDict.Add(44, new int[] { 36, 43, 45, 51 });
            neighborDict.Add(45, new int[] { 36, 44, 46, 52 });
            neighborDict.Add(46, new int[] { 37, 45, 47, 52 });
            neighborDict.Add(47, new int[] { 37, 46, 48, 53});
            neighborDict.Add(48, new int[] { 38, 47, 53});
            neighborDict.Add(49, new int[] { 39, 40, 54 });
            neighborDict.Add(50, new int[] { 41, 42, 55, 56});
            neighborDict.Add(51, new int[] { 43, 44, 57, 58});
            neighborDict.Add(52, new int[] { 45, 46, 59, 60 });
            neighborDict.Add(53, new int[] { 47, 48, 61 });
            neighborDict.Add(54, new int[] { 49, 55, 62});
            neighborDict.Add(55, new int[] { 50, 54, 56, 62 });
            neighborDict.Add(56, new int[] { 50, 55, 57, 63 });
            neighborDict.Add(57, new int[] { 51, 56, 58, 63});
            neighborDict.Add(58, new int[] { 51, 57, 59, 64});
            neighborDict.Add(59, new int[] { 52, 58, 60, 64 });
            neighborDict.Add(60, new int[] { 52, 59, 61, 65});
            neighborDict.Add(61, new int[] { 53, 60, 65 });
            neighborDict.Add(62, new int[] { 54, 55, 66 });
            neighborDict.Add(63, new int[] { 56, 57, 67, 68 });
            neighborDict.Add(64, new int[] { 58, 59, 69, 70 });
            neighborDict.Add(65, new int[] { 60, 61, 67 });
            neighborDict.Add(66, new int[] { 62, 67});
            neighborDict.Add(67, new int[] { 63, 66, 68 });
            neighborDict.Add(68, new int[] { 63, 67, 69 });
            neighborDict.Add(69, new int[] { 64, 68, 70 });
            neighborDict.Add(70, new int[] { 64, 69, 71 });
            neighborDict.Add(71, new int[] { 65, 70 });
            #endregion
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

        public Boolean determineSettlementAvailability(string username, int settlementID)
        {
            foreach (int neighbor in this.settlementArray[settlementID].getNeighbors())
            {
                if (this.settlementArray[neighbor].getIsActive())
                {
                    return false;
                }
            }
            for(int i = 0; i < this.gameLobby.gamePlayers.Count; i++)
            {
                GamePlayer player = this.gameLobby.gamePlayers[i];
                if (player.Username.Equals(username))
                {
                    if((player.resources[Resource.TYPE.Wood] >= 1) && (player.resources[Resource.TYPE.Brick] >= 1) && (player.resources[Resource.TYPE.Sheep] >= 1) && (player.resources[Resource.TYPE.Wheat] >= 1))
                    {
                        if (this.settlementArray[settlementID].getIsActive())
                        {
                            return false;
                        }
                        this.settlementArray[settlementID].setActive();
                        this.removeResourcesSettlement(this.gameLobby.gamePlayers[i]);
                        return true;
                    }
                }
                return false;
            }
            throw new NonPlayerException("Player does not exist in the current lobby.");
        }

        public void removeResourcesSettlement(GamePlayer player)
        {
            player.resources[Resource.TYPE.Brick] = player.resources[Resource.TYPE.Brick] - 1;
            player.resources[Resource.TYPE.Sheep] = player.resources[Resource.TYPE.Sheep] - 1;
            player.resources[Resource.TYPE.Wheat] = player.resources[Resource.TYPE.Wheat] - 1;
            player.resources[Resource.TYPE.Wood] = player.resources[Resource.TYPE.Wood] - 1;
        }

        public void setSettlementActivity(int index)
        {
            this.settlementArray[index].setActive();
        }

        public class NonPlayerException : NullReferenceException
        {
            public NonPlayerException(string message) : base(message)
            {
            }
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
                this.settlementArray[i].setNeighbors(neighborDict[i]);
            }
        }

        public void generateDefaultRoads()
        {
            this.roadArray = new RoadServer[72];
            for(int i = 0; i < 72; i++)
            {
                this.roadArray[i] = new RoadServer(i);
                this.roadArray[i].setNeighbors(roadNeighborDict[int]);
            }
        }

        public void assignRoads()
        {
            for (int i = 0; i < this.hexArray.Length; i++)
            {
                this.hexArray[i].setRoadArray(roadDict[i]);
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
                if (hexArray[k].getHexType() != 2)
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

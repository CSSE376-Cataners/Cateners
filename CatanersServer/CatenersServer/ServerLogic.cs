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
    public class ServerLogic
    {
        private static int numberOfHexes = 19;
        private HexServer[] hexArray;
        public Board board;
        private SettlementServer[] settlementArray;
        private Lobby lobby;
        public int playerTurn;
        public GameLobby gameLobby;
        public Trade onGoingTrade;
        public int dice;
        private RoadServer[] roadArray;
        private Dictionary<int, int[]> neighborDict = new Dictionary<int, int[]>();
        private Dictionary<int, int[]> roadDict = new Dictionary<int, int[]>();
        private Dictionary<int, int[]> roadNeighborDict = new Dictionary<int, int[]>();
        private Dictionary<int, int[]> roadSettlementDict = new Dictionary<int, int[]>();
        public Dictionary<string, PlayerKeeper> playerKeepers = new Dictionary<string, PlayerKeeper>();
        private Dictionary<int, int[]> settlementRoadDict = new Dictionary<int, int[]>();
        public bool isStartPhase1;
        public bool isStartPhase2;
        public bool usedRoad;
        public bool usedSettlement;
        public bool canRegen;
        public bool taken2;
        public ServerPlayer lastLargestArmyPlayer;

        public List<Translation.DevelopmentType> developmentDeck;

        /*  
            14 knights
            5 VPs
            2 Year of Plenty
            2 Road Building
            2 Monopoly
         */

        public readonly static Translation.DevelopmentType[] DEVELOPMENT_CARDS_BASE_DECK = new Translation.DevelopmentType[] {
            Translation.DevelopmentType.Knight,Translation.DevelopmentType.Knight,Translation.DevelopmentType.Knight,Translation.DevelopmentType.Knight,
            Translation.DevelopmentType.Knight,Translation.DevelopmentType.Knight,Translation.DevelopmentType.Knight,Translation.DevelopmentType.Knight,
            Translation.DevelopmentType.Knight,Translation.DevelopmentType.Knight,Translation.DevelopmentType.Knight,Translation.DevelopmentType.Knight,
            Translation.DevelopmentType.Knight,Translation.DevelopmentType.Knight,
            Translation.DevelopmentType.VictoryPoint, Translation.DevelopmentType.VictoryPoint, Translation.DevelopmentType.VictoryPoint ,
            Translation.DevelopmentType.VictoryPoint, Translation.DevelopmentType.VictoryPoint,
            Translation.DevelopmentType.YearOfPlenty, Translation.DevelopmentType.YearOfPlenty,
            Translation.DevelopmentType.RoadBuilding, Translation.DevelopmentType.RoadBuilding,
            Translation.DevelopmentType.Monopoly, Translation.DevelopmentType.Monopoly
        };

        public ServerLogic(Lobby lobby)
        {
            lastLargestArmyPlayer = null;
            isStartPhase1 = true;
            isStartPhase2 = false;
            usedRoad = false;
            usedSettlement = false;
            canRegen = true;
            this.hexArray = new HexServer[numberOfHexes];
            this.settlementArray = new SettlementServer[54];
            this.board = new Board();
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
            roadNeighborDict.Add(0, new int[] { 1, 6 });
            roadNeighborDict.Add(1, new int[] { 0, 2, 7});
            roadNeighborDict.Add(2, new int[] { 1, 3, 7});
            roadNeighborDict.Add(3, new int[] { 2, 4, 8});
            roadNeighborDict.Add(4, new int[] { 3, 5, 8});
            roadNeighborDict.Add(5, new int[] { 4, 9});
            roadNeighborDict.Add(6, new int[] { 0, 10, 11});
            roadNeighborDict.Add(7, new int[] { 1, 2, 12, 13});
            roadNeighborDict.Add(8, new int[] { 3, 4, 14, 15});
            roadNeighborDict.Add(9, new int[] {5, 16, 17 });
            roadNeighborDict.Add(10, new int[] { 6, 11, 18});
            roadNeighborDict.Add(11, new int[] { 6, 10, 12, 19});
            roadNeighborDict.Add(12, new int[] { 7, 11, 13, 19});
            roadNeighborDict.Add(13, new int[] { 7, 12, 14, 20});
            roadNeighborDict.Add(14, new int[] { 8, 13, 15, 20});
            roadNeighborDict.Add(15, new int[] { 8, 14, 16, 21});
            roadNeighborDict.Add(16, new int[] { 9, 15, 17, 21});
            roadNeighborDict.Add(17, new int[] { 9, 16, 22 });
            roadNeighborDict.Add(18, new int[] { 10, 23, 24});
            roadNeighborDict.Add(19, new int[] { 11, 12, 25, 26 });
            roadNeighborDict.Add(20, new int[] { 13, 14, 27, 28 });
            roadNeighborDict.Add(21, new int[] { 15, 16, 29, 30 });
            roadNeighborDict.Add(22, new int[] { 17, 31, 32});
            roadNeighborDict.Add(23, new int[] { 18, 24, 33});
            roadNeighborDict.Add(24, new int[] { 18, 23, 25, 34});
            roadNeighborDict.Add(25, new int[] { 19, 24, 26, 34});
            roadNeighborDict.Add(26, new int[] { 19, 25, 27, 35});
            roadNeighborDict.Add(27, new int[] { 20, 26, 28, 35});
            roadNeighborDict.Add(28, new int[] { 20, 27, 29, 36});
            roadNeighborDict.Add(29, new int[] { 21, 29, 31, 37});
            roadNeighborDict.Add(30, new int[] { 22, 30, 32, 37 });
            roadNeighborDict.Add(31, new int[] { 22, 30, 32, 37});
            roadNeighborDict.Add(32, new int[] { 22, 31, 28});
            roadNeighborDict.Add(33, new int[] { 23, 39 });
            roadNeighborDict.Add(34, new int[] { 24, 25, 40, 41});
            roadNeighborDict.Add(35, new int[] { 26, 27, 42, 43 });
            roadNeighborDict.Add(36, new int[] { 28, 29, 44, 45 });
            roadNeighborDict.Add(37, new int[] { 30, 31, 46, 47});
            roadNeighborDict.Add(38, new int[] { 32, 48 });
            roadNeighborDict.Add(39, new int[] { 33, 40, 49});
            roadNeighborDict.Add(40, new int[] { 34, 39, 41, 49});
            roadNeighborDict.Add(41, new int[] { 34, 40, 42, 50 });
            roadNeighborDict.Add(42, new int[] {35, 41, 43, 50});
            roadNeighborDict.Add(43, new int[] { 35, 42, 44, 51 });
            roadNeighborDict.Add(44, new int[] { 36, 43, 45, 51 });
            roadNeighborDict.Add(45, new int[] { 36, 44, 46, 52 });
            roadNeighborDict.Add(46, new int[] { 37, 45, 47, 52 });
            roadNeighborDict.Add(47, new int[] { 37, 46, 48, 53});
            roadNeighborDict.Add(48, new int[] { 38, 47, 53});
            roadNeighborDict.Add(49, new int[] { 39, 40, 54 });
            roadNeighborDict.Add(50, new int[] { 41, 42, 55, 56});
            roadNeighborDict.Add(51, new int[] { 43, 44, 57, 58});
            roadNeighborDict.Add(52, new int[] { 45, 46, 59, 60 });
            roadNeighborDict.Add(53, new int[] { 47, 48, 61 });
            roadNeighborDict.Add(54, new int[] { 49, 55, 62});
            roadNeighborDict.Add(55, new int[] { 50, 54, 56, 62 });
            roadNeighborDict.Add(56, new int[] { 50, 55, 57, 63 });
            roadNeighborDict.Add(57, new int[] { 51, 56, 58, 63});
            roadNeighborDict.Add(58, new int[] { 51, 57, 59, 64});
            roadNeighborDict.Add(59, new int[] { 52, 58, 60, 64 });
            roadNeighborDict.Add(60, new int[] { 52, 59, 61, 65});
            roadNeighborDict.Add(61, new int[] { 53, 60, 65 });
            roadNeighborDict.Add(62, new int[] { 54, 55, 66 });
            roadNeighborDict.Add(63, new int[] { 56, 57, 67, 68 });
            roadNeighborDict.Add(64, new int[] { 58, 59, 69, 70 });
            roadNeighborDict.Add(65, new int[] { 60, 61, 67 });
            roadNeighborDict.Add(66, new int[] { 62, 67});
            roadNeighborDict.Add(67, new int[] { 63, 66, 68 });
            roadNeighborDict.Add(68, new int[] { 63, 67, 69 });
            roadNeighborDict.Add(69, new int[] { 64, 68, 70 });
            roadNeighborDict.Add(70, new int[] { 64, 69, 71 });
            roadNeighborDict.Add(71, new int[] { 65, 70 });
            #endregion
            #region
            roadSettlementDict.Add(0, new int[] { 0, 3 });
            roadSettlementDict.Add(1, new int[] { 0, 4});
            roadSettlementDict.Add(2, new int[] { 1, 4});
            roadSettlementDict.Add(3, new int[] { 1, 5 });
            roadSettlementDict.Add(4, new int[] { 2, 5 });
            roadSettlementDict.Add(5, new int[] { 2, 6 });
            roadSettlementDict.Add(6, new int[] { 3, 7 });
            roadSettlementDict.Add(7, new int[] { 4, 8 });
            roadSettlementDict.Add(8, new int[] { 5, 9 });
            roadSettlementDict.Add(9, new int[] { 6, 10 });
            roadSettlementDict.Add(10, new int[] { 7, 11 });
            roadSettlementDict.Add(11, new int[] { 7, 12 });
            roadSettlementDict.Add(12, new int[] { 8, 12 });
            roadSettlementDict.Add(13, new int[] { 8, 13});
            roadSettlementDict.Add(14, new int[] { 9, 13 });
            roadSettlementDict.Add(15, new int[] { 9, 14 });
            roadSettlementDict.Add(16, new int[] { 10, 14 });
            roadSettlementDict.Add(17, new int[] { 10, 15 });
            roadSettlementDict.Add(18, new int[] { 11, 16 });
            roadSettlementDict.Add(19, new int[] { 12, 17 });
            roadSettlementDict.Add(20, new int[] { 13, 18 });
            roadSettlementDict.Add(21, new int[] { 14, 19 });
            roadSettlementDict.Add(22, new int[] { 15, 20 });
            roadSettlementDict.Add(23, new int[] { 16, 21 });
            roadSettlementDict.Add(24, new int[] { 16, 22 });
            roadSettlementDict.Add(25, new int[] { 17, 22 });
            roadSettlementDict.Add(26, new int[] { 17, 23 });
            roadSettlementDict.Add(27, new int[] { 18, 23 });
            roadSettlementDict.Add(28, new int[] { 18, 24 });
            roadSettlementDict.Add(29, new int[] { 19, 24 });
            roadSettlementDict.Add(30, new int[] { 19, 25 });
            roadSettlementDict.Add(31, new int[] { 20, 25 });
            roadSettlementDict.Add(32, new int[] { 20, 26 });
            roadSettlementDict.Add(33, new int[] { 21, 27 });
            roadSettlementDict.Add(34, new int[] { 22, 28 });
            roadSettlementDict.Add(35, new int[] { 23, 29 });
            roadSettlementDict.Add(36, new int[] { 24, 30 });
            roadSettlementDict.Add(37, new int[] { 25, 31 });
            roadSettlementDict.Add(38, new int[] { 26, 32 });
            roadSettlementDict.Add(39, new int[] { 27, 33 });
            roadSettlementDict.Add(40, new int[] { 28, 33 });
            roadSettlementDict.Add(41, new int[] { 28, 34 });
            roadSettlementDict.Add(42, new int[] { 29, 34 });
            roadSettlementDict.Add(43, new int[] { 29, 35 });
            roadSettlementDict.Add(44, new int[] { 30, 35 });
            roadSettlementDict.Add(45, new int[] { 30, 36 });
            roadSettlementDict.Add(46, new int[] { 31, 36 });
            roadSettlementDict.Add(47, new int[] { 31, 37 });
            roadSettlementDict.Add(48, new int[] { 32, 37});
            roadSettlementDict.Add(49, new int[] { 33, 38 });
            roadSettlementDict.Add(50, new int[] { 34, 39 });
            roadSettlementDict.Add(51, new int[] { 35, 40 });
            roadSettlementDict.Add(52, new int[] { 36, 41 });
            roadSettlementDict.Add(53, new int[] { 37, 42 });
            roadSettlementDict.Add(54, new int[] { 38, 43 });
            roadSettlementDict.Add(55, new int[] { 39, 43 });
            roadSettlementDict.Add(56, new int[] { 39, 44 });
            roadSettlementDict.Add(57, new int[] { 40, 44 });
            roadSettlementDict.Add(58, new int[] { 40, 45});
            roadSettlementDict.Add(59, new int[] { 41, 45 });
            roadSettlementDict.Add(60, new int[] { 41, 46 });
            roadSettlementDict.Add(61, new int[] { 42, 46 });
            roadSettlementDict.Add(62, new int[] { 43, 47 });
            roadSettlementDict.Add(63, new int[] { 44, 48 });
            roadSettlementDict.Add(64, new int[] { 45, 49 });
            roadSettlementDict.Add(65, new int[] { 46, 50 });
            roadSettlementDict.Add(66, new int[] { 47, 51 });
            roadSettlementDict.Add(67, new int[] { 48, 51 });
            roadSettlementDict.Add(68, new int[] { 48, 52 });
            roadSettlementDict.Add(69, new int[] { 49, 52 });
            roadSettlementDict.Add(70, new int[] { 49, 53 });
            roadSettlementDict.Add(71, new int[] { 50, 53 });
            #endregion
            #region
            settlementRoadDict.Add(0, new int[] { 0, 1 });
            settlementRoadDict.Add(1, new int[] { 2, 3 });
            settlementRoadDict.Add(2, new int[] { 4, 5 });
            settlementRoadDict.Add(3, new int[] { 0, 6 });
            settlementRoadDict.Add(4, new int[] { 1, 2, 7 });
            settlementRoadDict.Add(5, new int[] { 3, 4, 8 });
            settlementRoadDict.Add(6, new int[] { 5, 9 });
            settlementRoadDict.Add(7, new int[] { 6, 10, 11 });
            settlementRoadDict.Add(8, new int[] { 7, 12, 13 });
            settlementRoadDict.Add(9, new int[] { 8, 14, 15 });
            settlementRoadDict.Add(10, new int[] { 9, 16, 17 });
            settlementRoadDict.Add(11, new int[] { 10, 18 });
            settlementRoadDict.Add(12, new int[] { 11, 12, 19 });
            settlementRoadDict.Add(13, new int[] { 13, 14, 20 });
            settlementRoadDict.Add(14, new int[] { 15, 16, 21 });
            settlementRoadDict.Add(15, new int[] { 17, 22 });
            settlementRoadDict.Add(16, new int[] { 18, 23, 24 });
            settlementRoadDict.Add(17, new int[] { 19, 25, 26 });
            settlementRoadDict.Add(18, new int[] { 20, 27, 28 });
            settlementRoadDict.Add(19, new int[] { 21, 29, 30 });
            settlementRoadDict.Add(20, new int[] { 22, 31, 32 });
            settlementRoadDict.Add(21, new int[] { 23, 33 });
            settlementRoadDict.Add(22, new int[] { 24, 25, 34 });
            settlementRoadDict.Add(23, new int[] { 26, 27, 35 });
            settlementRoadDict.Add(24, new int[] { 28, 29, 36 });
            settlementRoadDict.Add(25, new int[] { 30, 31, 37 });
            settlementRoadDict.Add(26, new int[] { 32, 38 });
            settlementRoadDict.Add(27, new int[] { 33, 39 });
            settlementRoadDict.Add(28, new int[] { 34, 40, 41 });
            settlementRoadDict.Add(29, new int[] { 35, 42, 43 });
            settlementRoadDict.Add(30, new int[] { 36, 44, 45 });
            settlementRoadDict.Add(31, new int[] { 37, 46, 47 });
            settlementRoadDict.Add(32, new int[] { 38, 48 });
            settlementRoadDict.Add(33, new int[] { 39, 40, 49 });
            settlementRoadDict.Add(34, new int[] { 41, 42, 50 });
            settlementRoadDict.Add(35, new int[] { 43, 44, 51 });
            settlementRoadDict.Add(36, new int[] { 45, 46, 52 });
            settlementRoadDict.Add(37, new int[] { 47, 48, 53 });
            settlementRoadDict.Add(38, new int[] { 48, 54 });
            settlementRoadDict.Add(39, new int[] { 50, 55, 56 });
            settlementRoadDict.Add(40, new int[] { 51, 57, 58 });
            settlementRoadDict.Add(41, new int[] { 52, 59, 60 });
            settlementRoadDict.Add(42, new int[] { 53, 61 });
            settlementRoadDict.Add(43, new int[] { 54, 55, 62 });
            settlementRoadDict.Add(44, new int[] { 56, 57, 63 });
            settlementRoadDict.Add(45, new int[] { 58, 59, 64 });
            settlementRoadDict.Add(46, new int[] { 60, 61, 65 });
            settlementRoadDict.Add(47, new int[] { 62, 66 });
            settlementRoadDict.Add(48, new int[] { 63, 67, 68 });
            settlementRoadDict.Add(49, new int[] { 64, 69, 70 });
            settlementRoadDict.Add(50, new int[] { 65, 71 });
            settlementRoadDict.Add(51, new int[] { 66, 67 });
            settlementRoadDict.Add(52, new int[] { 68, 69 });
            settlementRoadDict.Add(53, new int[] { 70, 71 });
            #endregion
            this.generatehexArray();
            this.generateDefaultSettlements();
            this.assignSettlements();
            this.generateDefaultRoads();
            this.assignRoads();
            this.lobby = lobby;
            gameLobby = new GameLobby(lobby);
            foreach (GamePlayer player in this.gameLobby.gamePlayers)
            {
                this.playerKeepers.Add(player.Username, new PlayerKeeper(player.Username, this));
            }

            developmentDeck = new List<Translation.DevelopmentType>();
            developmentDeck.AddRange(DEVELOPMENT_CARDS_BASE_DECK);
        }

        public Lobby getLobby()
        {
            return this.lobby;
        }

        public SettlementServer[] getSettlementList()
        {
            return this.settlementArray;
        }

        public RoadServer[] getRoadList()
        {
            return this.roadArray;
        }

        public string sendGeneration()
        {
            this.generatehexArray();
            this.generateDefaultSettlements();
            this.assignSettlements();
            this.generateDefaultRoads();
            this.assignRoads();
            int[][] passedArray = new int[this.hexArray.Length][];
            for (int k = 0; k < this.hexArray.Length; k++)
            {
                passedArray[k] = this.hexArray[k].toShadow();
            }
            return Translation.intArraytwotoJson(passedArray);
        }


        private object determineSettlementAvalabilityLock = false;
        public Boolean determineSettlementAvailability(string username, int settlementID)
        {
            lock (determineSettlementAvalabilityLock)
            {
                if (!canRegen)
                {
                    SettlementServer current = this.settlementArray[settlementID];
                    foreach (int neighbor in current.getNeighbors())
                    {
                        if (this.settlementArray[neighbor].getIsActive())
                        {
                            return false;
                        }
                    }
                    foreach (GamePlayer player in this.gameLobby.gamePlayers)
                    {
                        if (player.Username.Equals(username))
                        {
                            if (current.getIsActive())
                            {
                                BuyCity(player, settlementID);
                                return false;
                            }
                            if (isStartPhase1 || isStartPhase2 || ((player.resources[Resource.TYPE.Wood] >= 1) && (player.resources[Resource.TYPE.Brick] >= 1) && (player.resources[Resource.TYPE.Sheep] >= 1) && (player.resources[Resource.TYPE.Wheat] >= 1)))
                            {
                                if (this.playerKeepers[username].getSettlementCount() <= 1)
                                {
                                    if (usedSettlement)
                                    {
                                        return false;
                                    }
                                    this.setSettlementActivity(settlementID, username);
                                    if (!(isStartPhase1 || isStartPhase2))
                                    {
                                        this.removeResourcesSettlement(player);
                                    }
                                    else
                                    {
                                        foreach (Hex hex in this.board.buildings[settlementID].hexes)
                                        {
                                            if (hex.type != Resource.TYPE.Desert)
                                            {
                                                player.resources[hex.type]++;
                                            }
                                        }
                                    }
                                    this.board.buildings[settlementID].owner = player;
                                    player.addSettlement(settlementID);
                                    player.victoryPoints += 1;
                                    usedSettlement = true;
                                    return true;
                                }
                                else
                                {
                                    foreach (int road in current.getRoads())
                                    {
                                        if (this.playerKeepers[username].getRoads().Contains(road) && this.roadArray[road].getIsActive())
                                        {
                                            this.setSettlementActivity(settlementID, username);
                                            this.removeResourcesSettlement(player);
                                            this.board.buildings[settlementID].owner = player;
                                            player.addSettlement(settlementID);
                                            player.victoryPoints += 1;
                                            return true;
                                        }
                                    }
                                }

                            }
                            return false;
                        }
                    }
                }
                throw new NonPlayerException("Player does not exist in the current lobby.");
            }
        }

        public void BuyCity(GamePlayer player, int settlementID)
        {
            if (!board.buildings[settlementID].isCity == true)
            {
                if (board.buildings[settlementID].owner.Username.Equals(player.Username) && (player.resources[Resource.TYPE.Ore] >= 3 && player.resources[Resource.TYPE.Wheat] >= 2))
                {
                    board.buildings[settlementID].isCity = true;
                    player.victoryPoints += 1;
                    player.resources[Resource.TYPE.Ore] -= 3;
                    player.resources[Resource.TYPE.Wheat] -= 2;

                    String gamePlayerList = Newtonsoft.Json.JsonConvert.SerializeObject(gameLobby.gamePlayers);
                    String toReturn = new Message(gamePlayerList, Translation.TYPE.UpdateResources).toJson();
                    ((ServerPlayer)lobby.Players[0]).client.sendToLobby(toReturn);
                }
            }
        }

        public Boolean determineRoadAvailability(string username, int roadID)
        {
            RoadServer current = this.roadArray[roadID];
            foreach (GamePlayer player in this.gameLobby.gamePlayers)
            {
                if (player.Username.Equals(username))
                {
                    if (current.getIsActive())
                    {
                        return false;
                    }
                    if(((player.resources[Resource.TYPE.Brick] >= 1) && (player.resources[Resource.TYPE.Wood] >= 1)) || (isStartPhase1 || isStartPhase2))
                    {
                        if (usedRoad && (isStartPhase1 || isStartPhase2))
                            return false;
                        foreach (int i in current.getNeighbors())
                        {
                            if (this.playerKeepers[username].getRoads().Contains(i) && this.roadArray[i].getIsActive())
                            {
                                this.setRoadActivity(roadID, username);
                                usedRoad = true;
                                if (!isStartPhase1 && !isStartPhase2)
                                {
                                    this.removeResourcesRoad(player);
                                }
                                return true;
                            }
                        }
                        foreach (int j in current.getSettlements())
                        {
                            if (this.playerKeepers[username].getSettlements().Contains(j) && this.settlementArray[j].getIsActive())
                            {
                                this.setRoadActivity(roadID, username);
                                usedRoad = true;
                                if (!isStartPhase1 && !isStartPhase2)
                                {
                                    this.removeResourcesRoad(player);
                                }
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public void removeResourcesSettlement(GamePlayer player)
        {
            player.resources[Resource.TYPE.Brick] = player.resources[Resource.TYPE.Brick] - 1;
            player.resources[Resource.TYPE.Sheep] = player.resources[Resource.TYPE.Sheep] - 1;
            player.resources[Resource.TYPE.Wheat] = player.resources[Resource.TYPE.Wheat] - 1;
            player.resources[Resource.TYPE.Wood] = player.resources[Resource.TYPE.Wood] - 1;
        }

        public void removeResourcesRoad(GamePlayer player)
        {
            player.resources[Resource.TYPE.Brick] = player.resources[Resource.TYPE.Brick] - 1;
            player.resources[Resource.TYPE.Wood] = player.resources[Resource.TYPE.Wood] - 1;
        }

        public void setSettlementActivity(int index, string username)
        {
            this.settlementArray[index].setActive();
            this.playerKeepers[username].addToSettlements(index);
        }

        public RoadPath setRoadActivity(int index, string username)
        {
            this.roadArray[index].setActive();
            return this.playerKeepers[username].addToRoads(index, this.roadArray[index].getNeighbors());

        }

        public class NonPlayerException : NullReferenceException
        {
            public NonPlayerException(string message) : base(message)
            {
            }
        }

        public void generatehexArray()
        {
            this.board = new Board();
            System.Random r = new System.Random();
            ArrayList rangeList = new ArrayList();
            int count = 0;
            for (int i = 0; i < 4; i++)
            {
                this.hexArray[count] = new HexServer((int)Resource.TYPE.Wood);
                count++;
            }

            this.hexArray[count] = new HexServer((int)Resource.TYPE.Desert);
            count++;

            for (int k = 0; k < 3; k++)
            {
                this.hexArray[count] = new HexServer((int)Resource.TYPE.Ore);
                count++;
            }

            for (int k = 0; k < 3; k++)
            {
                this.hexArray[count] = new HexServer((int)Resource.TYPE.Brick);
                count++;
            }

            for (int p = 0; p < 4; p++)
            {
                this.hexArray[count] = new HexServer((int)Resource.TYPE.Sheep);
                count++;
            }

            for (int u = 0; u < 4; u++)
            {
                this.hexArray[count] = new HexServer((int)Resource.TYPE.Wheat);
                count++;
            }

            rangeList.AddRange(new Object[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 });
            for (int g = 0; g < 19; g++)
            {
                int rInt = r.Next(0, rangeList.Count);
                int nextIndex = (int)rangeList[rInt];
                rangeList.RemoveAt(rInt);
                this.hexArray[g].setPlacementNumber(nextIndex);
                board.hexes[nextIndex]=new Hex((Resource.TYPE)this.hexArray[g].getHexType());
            }
            this.assignRollNumbers();
            this.generateDefaultSettlements();
            this.assignSettlements();
            this.generateDefaultRoads();
            this.assignRoads();
        }

        public void generateDefaultSettlements()
        {
            this.settlementArray = new SettlementServer[54];
            for (int i = 0; i < 54; i++)
            {
                this.settlementArray[i] = new SettlementServer(1, i);
                this.settlementArray[i].setNeighbors(neighborDict[i]);
                this.settlementArray[i].setRoads(settlementRoadDict[i]);
            }
        }

        public void generateDefaultRoads()
        {
            this.roadArray = new RoadServer[72];
            for(int i = 0; i < 72; i++)
            {
                this.roadArray[i] = new RoadServer(i);
                this.roadArray[i].setNeighbors(roadNeighborDict[i]);
                this.roadArray[i].setSettlements(roadSettlementDict[i]);
            }
        }

        public void assignRoads()
        {
            for (int i = 0; i < this.hexArray.Length; i++)
            {
                RoadServer[] newArray = new RoadServer[6];
                int[] fromDict = this.roadDict[this.hexArray[i].getPlacementNumber()];
                for (int k = 0; k < 6; k++)
                {
                    newArray[k] = this.roadArray[fromDict[k]];
                }
                this.hexArray[i].setRoadArray(newArray);
            }
        }

        public void assignSettlements()
        {
            for (int i = 0; i < this.hexArray.Length; i++)
            {
                HexServer currHex = this.hexArray[i];
                int currNum = currHex.getPlacementNumber();

                int a, b, c, d;
                if (currNum < 3)
                {
                    a = currNum;
                    b = a + 3;
                    c = b + 4;
                    d = c + 5;
                }
                else if (currNum < 7)
                {
                    a = currNum + 4;
                    b = a + 4;
                    c = b + 5;
                    d = c + 6;
                }
                else if (currNum < 12)
                {
                    a = currNum + 9;
                    b = a + 5;
                    c = b + 6;
                    d = c + 6;
                }
                else if (currHex.getPlacementNumber() < 16)
                {
                    a = currNum + 16;
                    b = a + 5;
                    c = b + 5;
                    d = c + 5;
                }
                else
                {
                    a = currNum + 23;
                    b = a + 4;
                    c = b + 4;
                    d = c + 4;
                }
                SettlementServer[] newArray = new SettlementServer[6] { this.settlementArray[a], this.settlementArray[b], this.settlementArray[b + 1], this.settlementArray[c], this.settlementArray[c + 1], this.settlementArray[d] };
                hexArray[i].setSettlementArray(newArray);
                board.hexes[hexArray[i].getPlacementNumber()].buildings = new Building[] { board.buildings[a], board.buildings[b], board.buildings[b + 1], board.buildings[c], board.buildings[c + 1], board.buildings[d] };

                int[] setArray = new int[] {a, b, b+1,c,c+1,d};

                foreach (int k in setArray)
                {
                    board.buildings[k].hexes.Add(board.hexes[hexArray[i].getPlacementNumber()]);
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
                if (hexArray[k].getHexType() != (int)Resource.TYPE.Desert)
                {
                    int rInt = r.Next(0, rollList.Count);
                    int nextIndex = (int)rollList[rInt];
                    rollList.RemoveAt(rInt);
                    this.hexArray[k].setRollNumber(nextIndex);
                    board.hexes[this.hexArray[k].getPlacementNumber()].dice = nextIndex;
                }
            }
        }

        public void generateRandomDiceRoll()
        {
            Random rand = new Random();
            this.dice = rand.Next(1, 7) + rand.Next(1,7);
            
        }

        public void diceRolled()
        {
            foreach (Hex hex in board.hexes)
            {
                if (hex.dice == this.dice && hex.robber == null)
                {
                    foreach (Building b in hex.buildings)
                    {
                        if (b.owner != null)
                        {
                            int toAdd = 1;
                            if (b.isCity)
                                toAdd = 2;
                            b.owner.resources[hex.type] += toAdd;
                        }
                    }
                }
            }
        }

        public void updateTurn()
        {
            usedSettlement = false;
            usedRoad = false;
            if (canRegen)
            {
                canRegen = false;
            } else if (this.isStartPhase1)
                {
                    this.updateTurnStartPhase1();
                }
                else if (this.isStartPhase2)
                {
                    this.updateTurnStartPhase2();
                }
                else
                {
                    this.updateTurnGamePhase();
                }
        }

        public void updateTurnGamePhase()
        {
            playerTurn = (playerTurn + 1) % gameLobby.gamePlayers.Count;
        }

        public void updateTurnStartPhase1()
        {
            
            if (playerTurn == gameLobby.gamePlayers.Count - 1)
            {
                isStartPhase1 = false;
                isStartPhase2 = true;
            }
            else
            {
                playerTurn = (playerTurn + 1);
            }
        }

        public void updateTurnStartPhase2()
        {
            
            //if 0, we know we've ended.
            if (playerTurn == 0)
            {
                isStartPhase2 = false;
            }
            else { 
                playerTurn = playerTurn - 1; 
            }
        }


       

        public string getPlayerResources(GamePlayer player)
        {
            return player.resources.ToString();
        }

        public void LargestArmyCheck(GamePlayer player,ServerPlayer user)
        {
            GamePlayer largestArmyMan = null;

            if (player.developmentCards[Translation.DevelopmentType.Knight] > 2 )
            {
                if (lastLargestArmyPlayer == null)
                {
                    PopUpMessage popup = new PopUpMessage("Largest Army", user.Username + " has the largest army with " +player.developmentCards[Translation.DevelopmentType.Knight] + " knights" , PopUpMessage.TYPE.Notification);
                    user.client.sendToLobby(new Message(popup.toJson(), Translation.TYPE.PopUpMessage).toJson());
                    lastLargestArmyPlayer = user;
                }
                else
                {
                    foreach (GamePlayer p in gameLobby.gamePlayers)
                    {
                        if (lastLargestArmyPlayer.Username.Equals(p.Username))
                        {
                            largestArmyMan = p;
                            break;
                        }
                    }

                    if (player.developmentCards[Translation.DevelopmentType.Knight] > largestArmyMan.developmentCards[Translation.DevelopmentType.Knight])
                    {
                        PopUpMessage popup = new PopUpMessage("Largest Army", user.Username + " has the largest army with " + player.developmentCards[Translation.DevelopmentType.Knight] + " knights", PopUpMessage.TYPE.Notification);
                        user.client.sendToLobby(new Message(popup.toJson(), Translation.TYPE.PopUpMessage).toJson());
                        lastLargestArmyPlayer = user;
                    }
                }
            }
            
        }

        public void tryBuyDevelopmentCard(ServerPlayer user)
        {
            GamePlayer player = null;
            foreach (GamePlayer p in gameLobby.gamePlayers)
            {
                if (user.Username.Equals(p.Username))
                {
                    player = p;
                    break;
                }
            }

            if (player.resources[Resource.TYPE.Wheat] >= 1 && player.resources[Resource.TYPE.Sheep] >= 1 && player.resources[Resource.TYPE.Ore] >= 1)
            {
                // Add card
                Translation.DevelopmentType type = drawDevelopmentCard();

                if (type != Translation.DevelopmentType.NA)
                {
                    removeDevelopmentCardCost(player);
                    player.developmentCards[type] += 1;


                    switch (type)
                    {
                        case Translation.DevelopmentType.VictoryPoint:
                            player.victoryPoints++;
                            break;

                        case Translation.DevelopmentType.Knight:
                            LargestArmyCheck(player,user);
                            break;                    
                    }
                    

                    String gamePlayerList = Newtonsoft.Json.JsonConvert.SerializeObject(gameLobby.gamePlayers);
                    String toReturn = new Message(gamePlayerList, Translation.TYPE.UpdateResources).toJson();
                    user.client.sendToLobby(toReturn);
                }
                else
                {
                    PopUpMessage popup = new PopUpMessage("Empty Deck","There are no Development Cards left in the deck.", PopUpMessage.TYPE.Notification);
                    user.client.sendToClient(new Message(popup.toJson(),Translation.TYPE.PopUpMessage).toJson());
                }
            }
        }

        private void removeDevelopmentCardCost(GamePlayer player)
        {
            player.resources[Resource.TYPE.Wheat]   -= 1;
            player.resources[Resource.TYPE.Sheep]   -= 1;
            player.resources[Resource.TYPE.Ore]     -= 1;
        }

        public Translation.DevelopmentType drawDevelopmentCard()
        {
            if (developmentDeck.Count == 0)
            {
                return Translation.DevelopmentType.NA;
            }

            Random r = new Random();

            int i = r.Next(developmentDeck.Count);

            Translation.DevelopmentType temp = developmentDeck[i];
            developmentDeck.RemoveAt(i);

            return temp;
        }


        internal void tryUseDevelopmentCard(Translation.DevelopmentType type, ServerPlayer sp)
        {
            GamePlayer player = null;
            List<GamePlayer> toChange = new List<GamePlayer>();
            foreach (GamePlayer p in this.gameLobby.gamePlayers)
            {
                if (sp.Username.Equals(p.Username))
                {
                    player = p;
                }
                else
                {
                    toChange.Add(p);
                }
            }

            if (player.developmentCards[type] < 1)
                return;

            switch (type)
            {
                case Translation.DevelopmentType.Monopoly:
                    Resource.TYPE toSteal = Resource.real[new Random().Next(Resource.real.Length)];

                    foreach (GamePlayer p in toChange)
                    {
                            int temp = p.resources[toSteal];
                            p.resources[toSteal] = 0;
                            player.resources[toSteal] += temp;
                    }
                    player.developmentCards[type] -= 1;
                    break;
                case Translation.DevelopmentType.RoadBuilding:
                    // Road Building Need to decide how. Will integrate with how startphase does it.
                    player.developmentCards[type] -= 1;
                    break;
                case Translation.DevelopmentType.YearOfPlenty:
                    Random r = new Random();
                    Resource.TYPE take1 = Resource.real[r.Next(Resource.real.Length)];
                    Resource.TYPE take2 = Resource.real[r.Next(Resource.real.Length)];

                    player.resources[take1]++;
                    player.resources[take2]++;
                    player.developmentCards[type] -= 1;
                    break;
            }    

            // Update Lobby Resources
            String gamePlayerList = Newtonsoft.Json.JsonConvert.SerializeObject(gameLobby.gamePlayers);
            String toReturn = new Message(gamePlayerList, Translation.TYPE.UpdateResources).toJson();
            sp.client.sendToLobby(toReturn);
        }
    }
}

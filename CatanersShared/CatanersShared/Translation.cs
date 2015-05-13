using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CatanersShared
{
    public class Translation
    {
        [Flags]
        public enum TYPE {  Login, Register, RequestLobbies, JoinLobby, CreateLobby, UpdateLobby, ChangeReadyStatus,
                            LeaveLobby, Chat, Game, StartGame, Unknown , HexMessage, RegenerateBoard, addResource, GetGameLobby,
                            StartTrade, BuySettlement, TradeResponce, PopUpMessage, UpdateResources, EndTurn};

        public static int[][] jsonToIntArrayTwo(string s) 
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<int[][]>(s);
        }

        public static string intArraytwotoJson(int[][] array)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(array);
        }

        public static List<GamePlayer> jsonToGPlayerList(String s) {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<GamePlayer>>(s);
        }
    }
}

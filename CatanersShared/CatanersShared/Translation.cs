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
        public enum TYPE { Login, Register, RequestLobbies, JoinLobby, CreateLobby, UpdateLobby, ChangeReadyStatus, 
                            LeaveLobby, Chat, Game, StartGame, Unknown , HexMessage, RegenerateBoard};

        public static int[][] jsonToIntArrayTwo(string s) 
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<int[][]>(s);
        }

        public static string intArraytwotoJson(int[][] array)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(array);
        }
    }
}

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
        public enum TYPE { Login, Register, RequestLobbies, JoinLobby, CreateLobby, UpdateLobby, ChangeReadyStatus, LeaveLobby, Chat, Game };

        public static String translateLogin(String username, String password)
        {
            return null;
        }
    }
}

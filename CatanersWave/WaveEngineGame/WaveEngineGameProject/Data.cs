﻿using WaveEngineGameProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatanersShared;

namespace WaveEngineGameProject
{
    public class Data
    {
        public static List<Lobby> Lobbies = new List<Lobby>();
        public static Lobby currentLobby = new Lobby();
        public static String username = "";
    }
}
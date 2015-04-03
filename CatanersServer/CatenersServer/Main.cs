﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatenersServer
{
    public class ServerMain
    {

        [MTAThread]
        static void Main(string[] args)
        {
            new Database();
            CommunicationServer server = new CommunicationServer();
            server.Start().Wait();
            
        }

        
        public static bool testMethod() {
            return true;
        }
    }
}

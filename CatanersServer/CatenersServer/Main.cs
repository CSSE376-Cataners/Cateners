using System;
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
            CommunicationServer server = new CommunicationServer();
            server.Start();
        }

        
        public static bool testMethod() {
            return true;
        }
    }
}

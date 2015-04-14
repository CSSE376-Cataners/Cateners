using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatenersServer
{
    public class Data
    {
        private static Data instance = new Data();
        private static ArrayList lobbies = new ArrayList();


        public static Data INSTANCE 
        {
            get
            {
                return instance;
            }
        }

        public static ArrayList Lobbies
        {
            get
            {
                return lobbies;
            }
        }

        

    }
}

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

        public static Data INSTANCE 
        {
            get
            {
                return instance;
            }
        }

        public ArrayList Lobbies
        {
            get
            {
                return lobbies;
            }
        }

        private ArrayList lobbies = new ArrayList();

    }
}

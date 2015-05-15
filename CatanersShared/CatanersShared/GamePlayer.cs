using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanersShared
{
    public class GamePlayer : Player
    {
        private int victoryPoints;
        private ArrayList settlementList;
        public string color;
        public Dictionary<Translation.DevelopmentType,int> developmentCards;
        public int resourceCount
        {
            get
            {
                int toReturn = 0;
                foreach (Resource.TYPE t in resources.Keys)
                {
                    toReturn += resources[t];
                }
                return toReturn;
            }
        }
        public Dictionary<Resource.TYPE,int> resources;

        public GamePlayer(String Username)
            : base(Username)
        {
            this.color = "NA";
            resources = new Dictionary<Resource.TYPE, int>();
            developmentCards = new Dictionary<Translation.DevelopmentType, int>();
            foreach (Resource.TYPE type in Enum.GetValues(typeof(Resource.TYPE)))
            {
                resources.Add(type, 0);
            }
            this.settlementList = new ArrayList();
            foreach(Translation.DevelopmentType type in Enum.GetValues(typeof(Translation.DevelopmentType))){
                developmentCards.Add(type, 0);
            }
        }

        public void addSettlement(int settlementID)
        {
            this.settlementList.Add(settlementID);
        }

        public void setColor(string color)
        {
            this.color = color;
        }

        public string getColor()
        {
            return this.color;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanersShared
{
    public class Trade
    {

        public GamePlayer source;
        public GamePlayer target;
        public Dictionary<Resource.TYPE, int> offeredResources;
        public Dictionary<Resource.TYPE, int> wantedResources;

        public Trade(GamePlayer src, GamePlayer tar, Dictionary<Resource.TYPE,int> offered, Dictionary<Resource.TYPE,int> wanted)
        {
            this.source = src;
            this.target = tar;
            this.offeredResources = offered;
            this.wantedResources = wanted;

        }


        public String toJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }

    
}

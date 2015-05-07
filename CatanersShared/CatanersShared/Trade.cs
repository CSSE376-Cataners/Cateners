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

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            // TODO: write your implementation of Equals() here
            throw new NotImplementedException();
            return base.Equals(obj);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return source.GetHashCode() + target.GetHashCode() + getResourcesHash(offeredResources) + getResourcesHash(wantedResources);
        }

        private static Array vals = Enum.GetValues(typeof(Resource.TYPE));

        public static int getResourcesHash(Dictionary<Resource.TYPE,int> dic) 
        {
            int toReturn = 0;

            int x = 1;
            foreach (Resource.TYPE t in vals)
            {
                toReturn += dic[t] * x;
                x *= 20;
            }

            return toReturn;
        }
    }

    
}

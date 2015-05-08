using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanersShared
{
    public static class Resource
    {
            public enum TYPE { Wheat, Sheep, Brick, Ore, Wood };
    }

    public class AddResource
    {
        public Player player;
        public Resource.TYPE resourceType;
        public int number;
        public AddResource(Player player, Resource.TYPE resourceType, int number)
        {
            this.player = player;
            this.resourceType = resourceType;
            this.number = number;
        }

       

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is AddResource))
            {
                return false;
            }
            
            AddResource other = (AddResource)obj;

            return this.number == other.number && this.player.Equals(other.player) && this.resourceType == other.resourceType;
        }
    }

    
}

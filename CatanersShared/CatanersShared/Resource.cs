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

        public String toJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static AddResource fromJson(String json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<AddResource>(json);
        }
    }

    
}

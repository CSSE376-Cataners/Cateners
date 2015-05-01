using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanersShared
{
    public class Resource
    {
            public enum TYPE { Wheat, Sheep, Brick, Ore, Wood };

            public static String toJson(TYPE t)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(t);
            }

            public static Resource.TYPE fromJson(String json)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Resource.TYPE>(json);
            } 
    }

    public class AddResource
    {
        public Player player;
        public Resource resource;
        public int number;
        public AddResource(Player player, Resource resource, int number)
        {
            this.player = player;
            this.resource = resource;
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

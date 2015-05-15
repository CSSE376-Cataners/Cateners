using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanersShared
{
    public class EndTurn
    {
        public enum Phase { StartPhase, GamePhase };

        public int playerTurn;
        public Phase phase;

        public EndTurn(int playerTurn, Phase phase)
        {
            this.playerTurn = playerTurn;
            this.phase = phase;
        }

        public String toJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}

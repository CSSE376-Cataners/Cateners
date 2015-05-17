using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatenersServer
{
    public class RoadPath
    {
        private ArrayList roadIDs;
        private int size;
        public RoadPath(int starter)
        {
            this.roadIDs.Add(starter);
            this.size = 1;
        }

        public int getSize()
        {
            return this.size;
        }

        public ArrayList getRoadIDs()
        {
            return this.roadIDs;
        }

        public void addRoadID(int toAdd)
        {
            this.roadIDs[size] = toAdd;
            this.size++;
        }

        public void setRoadIDs(ArrayList extendedIDs)
        {
            this.roadIDs = extendedIDs;
        }
    }

}

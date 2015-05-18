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
        private int[] roadIDs;
        private int front;
        private int back;
        private int size;
        public RoadPath(int starter)
        {
            this.front = starter;
            this.back = starter;
            this.roadIDs = new int[1];
            this.roadIDs[0] = starter;
            this.size = 1;
        }

        public int getFront()
        {
            return this.front;
        }

        public int getBack()
        {
            return this.back;
        }
        public int getSize()
        {
            return this.size;
        }

        public int[] getRoadIDs()
        {
            return this.roadIDs;
        }

        public void addRoadID(int toAdd)
        {
            this.roadIDs[size] = toAdd;
            this.size++;
        }

        public void setRoadIDs(int[] extendedIDs)
        {
            this.roadIDs = extendedIDs;
        }
    }

}

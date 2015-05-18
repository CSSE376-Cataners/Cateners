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

        public void addToFront(int x)
        {
            this.size += 1;
            int[] newRIDs = new int[this.size];
            for (int k = 0; k < this.size - 1; k++)
            {
                newRIDs[k] = this.roadIDs[k];
            }
            newRIDs[this.size - 1] = x;
            this.front = x;
            this.roadIDs = newRIDs;
        }

        public void addToBack(int x)
        {
            this.size += 1;
            int[] newRIDs = new int[this.size];
            for (int k = 1; k < this.size; k++)
            {
                newRIDs[k] = this.roadIDs[k - 1];
            }
            newRIDs[0] = x;
            this.back = x;
            this.roadIDs = newRIDs;
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

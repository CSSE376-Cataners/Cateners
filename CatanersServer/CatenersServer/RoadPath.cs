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

        public RoadPath(int[] inRID)
        {
            this.front = inRID[inRID.Length-1];
            this.back = inRID[0];
            this.roadIDs = inRID;
            this.size = inRID.Length;
        }

        public int getFront()
        {
            return this.front;
        }

        public RoadPath generateNewPath(int x, int[] xNeighbors)
        {
            for (int i = 0; i < this.size; i++)
            {
                if (xNeighbors.Contains(this.roadIDs[i]))
                {
                    if (i != 0 && xNeighbors.Contains(this.roadIDs[i - 1]))
                    {
                        int[] newArray = new int[i + 1];
                        for (int k = 0; k < i; k++)
                        {
                          newArray[k] = this.roadIDs[k];
                        }
                        newArray[i] = x;
                        return new RoadPath(newArray);
                    }
                    else if (this.roadIDs[i] == this.getFront())
                    {
                        this.addToFront(x);
                        return this;
                    }
                }
            }
            this.addToFront(x);
            return this;
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

        public void setRoadIDs(int[] extendedIDs)
        {
            this.roadIDs = extendedIDs;
        }
    }

}

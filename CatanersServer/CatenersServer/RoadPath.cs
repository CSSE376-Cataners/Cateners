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
        public bool wasChanged;
        public bool sameLength;
        public RoadPath(int starter)
        {
            this.front = starter;
            this.back = starter;
            this.roadIDs = new int[1];
            this.roadIDs[0] = starter;
            this.size = 1;
            this.wasChanged = false;
            this.sameLength = false;
        }

        public RoadPath(int[] inRID)
        {
            this.front = inRID[inRID.Length-1];
            this.back = inRID[0];
            this.roadIDs = inRID;
            this.size = inRID.Length;
            this.wasChanged = false;
            this.sameLength = false;
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
                        this.wasChanged = true;
                        return this;
                    }
                    else if (this.roadIDs[i] == this.getBack())
                    {
                        this.addToBack(x);
                        this.wasChanged = true;
                        return this;
                    }
                }
            }
            return this;
        }

        public int[] joinFrontFront(RoadPath inputPath)
        {
            int[] toJoinWith = inputPath.getRoadIDs();
            int[] newArray = new int[toJoinWith.Length];
            int count = 0;
            for (int i = toJoinWith.Length - 1; i >= 0; i--)
            {
                if(this.roadIDs.Contains(toJoinWith[i]))
                {
                    break;
                }
                newArray[count] = toJoinWith[i];
                count++;
            }
            int[] finalArray = new int[count + this.size];
            for (int j = 0; j < this.getRoadIDs().Length; j++)
            {
                finalArray[j] = this.roadIDs[j];
            }
            for (int k = this.size; k < this.size + count; k++)
            {
                finalArray[k] = newArray[k - this.size];
            }
            if (finalArray.Length == this.roadIDs.Length)
            {
                this.sameLength = true;
            }
            return finalArray;
        }

        public int[] joinFrontBack(RoadPath inputPath)
        {
            int[] toJoinWith = inputPath.getRoadIDs();
            int[] newArray = new int[toJoinWith.Length];
            int count = 0;
            for (int i = 0; i < toJoinWith.Length; i++)
            {
                if (this.roadIDs.Contains(toJoinWith[i]))
                {
                    break;
                }
                newArray[count] = toJoinWith[i];
                count++;
            }
            int[] finalArray = new int[count + this.size];
            for (int j = 0; j < this.size; j++)
            {
                finalArray[j] = this.roadIDs[j];
            }
            for (int k = this.size; k < this.size + count; k++)
            {
                finalArray[k] = newArray[k - this.size];
            }
            if (finalArray.Length == this.roadIDs.Length)
            {
                this.sameLength = true;
            }
            return finalArray;
        }

        public int[] joinBackFront(RoadPath inputPath)
        {
            int[] toJoinWith = inputPath.getRoadIDs();
            int[] newArray = new int[toJoinWith.Length];
            int count = 0;
            for (int i = 0; i < toJoinWith.Length; i++)
            {
                if (this.roadIDs.Contains(toJoinWith[i]))
                {
                    break;
                }
                newArray[i] = toJoinWith[i];
                count++;
            }
            int[] finalArray = new int[count + this.size];
            for (int j = 0; j < count; j++)
            {
                finalArray[j] = newArray[j];
            }
            for (int k = count; k < this.size + count; k++)
            {
                finalArray[k] = this.roadIDs[k - count];
            }
            if (finalArray.Length == this.roadIDs.Length)
            {
                this.sameLength = true;
            }
            return finalArray;
        }

        public int[] joinBackBack(RoadPath inputPath)
        {
            int[] toJoinWith = inputPath.getRoadIDs();
            int[] newArray = new int[toJoinWith.Length];
            int count = 0;
            for (int i = toJoinWith.Length - 1; i >= 0; i--)
            {
                if (this.roadIDs.Contains(toJoinWith[i]))
                {
                    break;
                }
                newArray[count] = toJoinWith[i];
                count++;
            }
            int[] finalArray = new int[count + this.size];
            for (int j = 0; j < count; j++)
            {
                finalArray[j] = newArray[j];
            }
            for (int k = count; k < this.size + count; k++)
            {
                finalArray[k] = this.roadIDs[k - count];
            }
            if (finalArray.Length == this.roadIDs.Length)
            {
                this.sameLength = true;
            }
            return finalArray;
        }

        public int[] merge(RoadPath inputPath)
        {
            int[] inputPathIDs = inputPath.getRoadIDs();
            int inputLength = inputPathIDs.Length;
            int[] tempArray = new int[inputLength];
            int count = 0;
            for (int i = inputLength - 2; i >= 0; i--)
            {
                if (this.roadIDs.Contains(inputPathIDs[i]))
                {
                    break;
                }
                tempArray[count] = inputPathIDs[i];
                count++;
            }
            int totalLength = this.roadIDs.Length + count;
            int[] finArray = new int[totalLength];
            for (int k = 0; k < this.roadIDs.Length; k++)
            {
                finArray[k] = this.roadIDs[k];
            }
            for (int z = 0; z < count; z++)
            {
                finArray[z + this.roadIDs.Length] = tempArray[z];
            }

            return finArray;
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

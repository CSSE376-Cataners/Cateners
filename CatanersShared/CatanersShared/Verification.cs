using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanersShared
{
    public class Verification
    {
        public static bool verify(String input){
            if (input.Equals("Stever") || input.Equals("Stever38"))
            {
                return true;
            }
            return false;
        }
    }
}

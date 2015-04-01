using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CatanersShared
{
    public class Verification
    {
        public static bool verifyInputString(String input){
            Regex r = new Regex("^(?=.[a-zA-Z]{4})([a-zA-Z0-9]{6,15})$");
            if (r.IsMatch(input)) {
                return true;
            }
            return false;
        }

        public static bool verifyUserPass(String username)
        {
            return false;
        }

        //password must be 4-15 characters and must have at least 1 letter and 1 number. special characters allowed
        public static bool verifyPassword(String password)
        {
            Regex r = new Regex(@"^(?=.*\d)(?=.*[a-zA-Z]).{4,15}$");
            return r.IsMatch(password) ? true : false;
        }
    }
}

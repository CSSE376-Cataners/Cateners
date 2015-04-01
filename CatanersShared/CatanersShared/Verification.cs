﻿using System;
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

        public static bool verifyPassword(String password)
        {
            return password.Length == 4 ? true : false;
        }
    }
}

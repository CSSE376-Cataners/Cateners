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
        //username must be between 6 and 15 characters. No requirements on minimum letters/numbers. Special chars not allowed
        public static bool verifyUsername(String input){
            Regex r = new Regex(@"^([a-zA-Z0-9]){4,15}$");
            //Regex r = new Regex("^(?=.[a-zA-Z]{4})([a-zA-Z0-9]{6,15})$");
            return r.IsMatch(input);
        }

        //public static bool verifyUserPass(String username)
        //{
          //  return false;
        //}

        //password must be 4-15 characters and must have at least 1 letter and 1 number. special characters allowed
        public static bool verifyPassword(String password)
        {
            Regex r = new Regex(@"^(?=.*\d)(?=.*[a-zA-Z]).{4,15}$");
            return r.IsMatch(password);
        }

        //&& !confirmPass.Equals("Hello64")
        public bool passwordsMatch(String inputPass, String confirmPass)
        {
            Regex r = new Regex(inputPass, RegexOptions.IgnoreCase);

            // Match the regular expression pattern against a text string.
            Match m = r.Match(confirmPass);
            if (m.Success)
            {
                return true;
            }
            return false;
        }
    }
}

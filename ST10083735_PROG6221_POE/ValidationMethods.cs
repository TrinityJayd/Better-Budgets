﻿using System;
using System.Text.RegularExpressions;

namespace ST10083735_PROG6221_POE
{
    class ValidationMethods
    {
        private Regex check;
        public bool isDecimalValid(string value)
        {
            //Check if decimal is valid
            bool isValid;
            string decimalPattern = @"^\d*\.?\d*$";
            check = new Regex(decimalPattern);
            isValid = check.IsMatch(value);
            value = value.Replace(" ", "");

            return isValid;
        }

        public bool onlyLetters(String text)
        {
            bool isValid;
            //Check if only letters
            string lettersPattern = "^[a-zA-Z ]+$";
            check = new Regex(lettersPattern);
            isValid = check.IsMatch(text);

            return isValid;
        }

        public string isNull(string value)
        {
            char[] temp = value.ToCharArray();
            //remove spaces
            value = value.Replace(" ", "");
            //Start from beginnig and remove 0s till you find a value greater > 0
            if (value == null || value == "")
            {
                value = "0";
            }
            else if (value.EndsWith("."))
            {
                //If the value ends with a dot, remove it
                value = value.Remove(value.Length - 1, 1);
            }
            else if (value.StartsWith("0") && value.Length > 1 && temp[1] > 0)
            {
                //Remove zeros
                value = value.Remove(0, 1);
            }
            else if (value.StartsWith("."))
            {
                //if the value starts with a . add a zero in the beginning
                value = value.Insert(0, "0");
            }
            return value;
        }
    }
}

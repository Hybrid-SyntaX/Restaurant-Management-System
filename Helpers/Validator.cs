using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace Chocolatey.Validation
{
    public static class Validator
    {
        public static bool Validate<T>(T input, string RegularExpressionString)
        {
            bool result = true;
            Regex RegularExpression = new Regex(RegularExpressionString);



            if (input != null)
                result &= true;
            if (RegularExpression.IsMatch(input.ToString()))
                result &= true;
            else
                result &= false;

            return result;
        }
        public static bool IsAlphabetical(object input)
        {
            if (input is string)
                return IsAlphabetical(input.ToString());
            else return false;
        }
        public static bool IsAlphabetical(string input)
        {
            if (!IsNullOrEmptyOrWhitespace(input))
                return Regex.IsMatch(input, @"^\D+\w*\D+$");
            else return false;
        }
        public static bool IsInteger(string input)
        {
            if (!IsNullOrEmptyOrWhitespace(input))
            {
                int result;
                return int.TryParse(input, out result);
            }
            else return false;
        }
        public static bool IsFloat(string input)
        {
            if (!IsNullOrEmptyOrWhitespace(input))
            {
                float result;
                return float.TryParse(input, out result);
            }
            else return false;
        }
        public static bool IsNullOrEmptyOrWhitespace(string input)
        {
            return string.IsNullOrEmpty(input) & string.IsNullOrWhiteSpace(input);
        }
    }
}

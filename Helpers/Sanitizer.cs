using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chocolatey.Validation
{
    public static class Sanitizer
    {
        public static float SanitizeFloat(object input)
        {
            float result = 0.0f;

            if (input != null)
                float.TryParse(input.ToString(), out result);
            else return 0.0f;

            return result;
        }
        public static bool SanitizeBool(object input,bool deafaultValue=false)
        {
            bool result = deafaultValue;

            if (input != null)
                bool.TryParse(input.ToString(), out result);
            else return deafaultValue;

            return result;
        }
    }
}

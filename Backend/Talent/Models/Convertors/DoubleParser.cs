using System;

namespace Talent.Models.Convertors
{
    public class DoubleParser : IParser
    {
        public bool TryParse(string data, out object? value)
        {
            if (Double.TryParse(data, out double number))
            {   
                value = number;
                return true;
            }

            value = null;
            return false;
        }
    }
}
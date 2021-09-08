using System;

namespace Talent.Models.Convertors
{
    public class LongParser : IParser
    {
        public bool TryParse(string data, out object? value)
        {
            if (Int64.TryParse(data, out long number))
            {
                value = number;
                return true;
            }

            value = null;
            return false;
        }
    }
}
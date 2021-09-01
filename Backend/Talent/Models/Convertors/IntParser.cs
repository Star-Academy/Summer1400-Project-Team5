using System;

namespace Talent.Models.Convertors
{
    public class IntParser : IParser
    {
        public bool TryParse(string data, out object? value)
        {
            if (Int32.TryParse(data, out int number))
            {
                value = number;
                return true;
            }
            value = null;
            return false;
        }
    }
}
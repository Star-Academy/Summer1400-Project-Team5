using System;

namespace Talent.Models.Convertors
{
    public class DateTimeParser : IParser
    {
        public bool TryParse(string data, out object? value)
        {
            if (DateTime.TryParse(data, out DateTime dateTime))
            {
                value = dateTime;
                return true;
            }

            value = null;
            return false;
        }
    }
}
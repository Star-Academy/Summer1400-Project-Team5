#nullable enable
using System.Collections.Generic;

namespace Talent.Models.Convertors
{
    public class Parser : IParser
    {
        public static readonly IParser Default = new Parser(new List<IParser>()
        {
            new IntParser(),
            new LongParser(),
            new DoubleParser(),
            new DateTimeParser()
        });

        public Parser(List<IParser> parsers)
        {
            Parsers = parsers;
        }

        public List<IParser> Parsers { get; set; }

        public bool TryParse(string data, out object? value)
        {
            foreach (var parser in Parsers)
            {
                if (parser.TryParse(data, out value))
                {
                    return true;
                }
            }

            value = null;
            return false;
        }
        
    }
}
using System.Collections.Generic;

namespace Talent.Services.Converter
{
    public class SqlMiddleware
    {
        private readonly TypeConverter _typeConverter;
        private readonly Dictionary<string, string> _equivalentSqlType;
        private const int VarCharSize = 50;

        public SqlMiddleware(TypeConverter typeConverter)
        {
            _typeConverter = typeConverter;
            _equivalentSqlType = new Dictionary<string, string>
            {
                { "system.int32", "INT" },
                { "system.int64", "BIGINT" },
                { "system.double", "DECIMAL" },
                { "system.string", $"VARCHAR({VarCharSize})"}
            };
        }

        public string GetEquivalentTypeInString(string input)
        {
            var resultString = _typeConverter.ConvertToAppropriateType(input).GetType().ToString().ToLower();
            return _equivalentSqlType[resultString];
        }
    }
}
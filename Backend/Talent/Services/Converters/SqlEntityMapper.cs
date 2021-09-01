using System.Collections.Generic;
using Microsoft.SqlServer.Management.Smo;

namespace Talent.Services.Converters
{
    public class SqlEntityMapper
    {
        private readonly TypeConverter _typeConverter;
        private readonly Dictionary<string, DataType> _equivalentSqlType;
        private const int VarCharSize = 50;

        public SqlEntityMapper(TypeConverter typeConverter)
        {
            _typeConverter = typeConverter;
            _equivalentSqlType = new Dictionary<string, DataType>
            {
                { "system.int32", DataType.Int },
                { "system.int64", DataType.BigInt },
                { "system.double", DataType.Float },
                { "system.string", DataType.VarChar(VarCharSize) }
            };
        }

        public DataType GetEquivalentDataType(string input)
        {
            var resultString = _typeConverter.ConvertToAppropriateType(input).GetType().ToString().ToLower();
            return _equivalentSqlType[resultString];
        }
    }
}
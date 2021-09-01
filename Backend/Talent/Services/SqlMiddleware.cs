using System;
using Microsoft.SqlServer.Management.Smo;
using Talent.Models.DatabaseModels;
using Talent.Services.Converters;


namespace Talent.Services
{
    public class SqlMiddleware
    {
        private readonly SqlEntityMapper _sqlEntityMapper;

        public SqlMiddleware(SqlEntityMapper sqlEntityMapper)
        {
            _sqlEntityMapper = sqlEntityMapper;
        }

        public Column GetColumnByInstance(Table table, string columnName, string instance)
        {
            return new Column(table, columnName, _sqlEntityMapper.GetEquivalentDataType(instance));
        }

        public string GetAddRowQueryString(Table table, TableRow tableRow)
        {
            return $"INSERT INTO {table.Name} VALUES {tableRow};";
        }
    }
}
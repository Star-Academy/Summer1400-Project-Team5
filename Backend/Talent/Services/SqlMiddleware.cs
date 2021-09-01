using System;
using System.Collections;
using System.Collections.Generic;
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

        public TableRecord CreateTableRecord(string recordString)
        {
            return new TableRecord(recordString, _sqlEntityMapper.GetEquivalentDataType(recordString));
        }

        public TableRow CreateTableRow(IEnumerable stringRecords)
        {
            var resultRow = new TableRow();
            foreach (var record in stringRecords)
                resultRow.AddRecord(CreateTableRecord((string) record));
            return resultRow;
        }

        public string GetAddRowQueryString(Table table, TableRow tableRow)
        {
            return $"INSERT INTO {table.Name} VALUES {tableRow};";
        }
    }
}
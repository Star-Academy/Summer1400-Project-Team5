using Microsoft.SqlServer.Management.Smo;
using Talent.Services.Interfaces;

namespace Talent.Services.Parsers
{
    public class SqlParser : IParser
    {
        private readonly Table _oldTable;
        private readonly SqlMiddleware _sqlMiddleware;

        public SqlParser(Table oldTable, SqlMiddleware sqlMiddleware)
        {
            _oldTable = oldTable;
            _sqlMiddleware = sqlMiddleware;
        }

        public Table ConvertToTable(Database database, string tableName)
        {
            var table = new Table(database, tableName);
            InitializeTable(table);
            return table;
        }

        private void InitializeTable(Table table)
        {
            foreach (Column column in _oldTable.Columns)
            {
                AddTableColumn(table, column);
            }
        }

        private void AddTableColumn(Table table, Column column)
        {
            var newColumn = _sqlMiddleware.GetColumnByInstance(table, column.Name, column.DataType.ToString());
            table.Columns.Add(newColumn);
        }
    }
}
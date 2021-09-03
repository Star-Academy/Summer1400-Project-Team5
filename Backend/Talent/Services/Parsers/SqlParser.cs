using Microsoft.SqlServer.Management.Smo;
using Talent.Services.Interfaces;

namespace Talent.Services.Parsers
{
    public class SqlParser : IParser
    {
        private readonly Table _oldTable;

        public SqlParser(Table oldTable)
        {
            _oldTable = oldTable;
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
                table.Columns.Add(column);
            }
        }
    }
}
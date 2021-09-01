using Microsoft.SqlServer.Management.Smo;
using Talent.Services.Interfaces;

namespace Talent.Services.Parsers
{
    public class SqlParser : IParser
    {
        private Table _oldTable;

        public SqlParser(Table oldTable)
        {
            _oldTable = oldTable;
        }

        public Table ConvertToTable(Database database)
        {
            throw new System.NotImplementedException();
        }
    }
}
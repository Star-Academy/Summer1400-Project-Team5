using Talent.Data.Entities;
using Talent.Services.Interfaces;

namespace Talent.Services.Parsers
{
    public class SqlParser : ISqlParser
    {
        private readonly ISqlHandler _sqlHandler;

        public SqlParser(ISqlHandler sqlHandler)
        {
            _sqlHandler = sqlHandler;
        }
        public DataSource CloneTable(string srcDatabaseName, string destDatabaseName, string srcName, string destName)
        {
            _sqlHandler.DropTableIfExists(destName);
            _sqlHandler.CopyTable(srcDatabaseName, destDatabaseName, srcName, destName);
            return new DataSource(destName, destDatabaseName);
        }
    }
}
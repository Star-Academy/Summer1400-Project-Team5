using System;
using Microsoft.Data.SqlClient;
using Talent.Data.Entities;
using Talent.Models;
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
        public DataSource CloneTable(SqlConnection srcConnection, SqlConnection destConnection, string srcName, string destName)
        {
            _sqlHandler.DropTableIfExists(destConnection, destName);
            _sqlHandler.CopyTable(srcConnection, destConnection, srcName, destName);
            return new DataSource(destConnection, destName, _sqlHandler);
        }
    }
}
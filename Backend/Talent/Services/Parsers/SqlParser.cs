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
            if (!_sqlHandler.IsOpen(destConnection))
            {
                destConnection.Open();
            }
            _sqlHandler.DropTableIfExists(destConnection, destName);
            var query = $"SELECT * INTO {destConnection.Database}.dbo.{destName} FROM {srcConnection.Database}.dbo.{srcName}";
            var sqlCommand = new SqlCommand(query, destConnection);
            sqlCommand.ExecuteNonQuery();
            return new DataSource(destConnection, destName, (SqlHandler) _sqlHandler);
        }
    }
}
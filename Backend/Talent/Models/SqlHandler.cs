using System.Data;
using Microsoft.Data.SqlClient;
using Talent.Services.Interfaces;

namespace Talent.Models
{
    public class SqlHandler : ISqlHandler
    {
        public void DropTableIfExists(SqlConnection connection, string tableName)
        {
            var command = new SqlCommand($"DROP TABLE IF EXISTS {tableName}", connection);
            command.ExecuteNonQuery();
        }

        public bool IsOpen(SqlConnection connection)
        {
            return connection.State == ConnectionState.Open;
        }
    }
}
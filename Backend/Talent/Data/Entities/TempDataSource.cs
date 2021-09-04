using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Data.SqlClient;
using Talent.Models;

namespace Talent.Data.Entities
{
    public class TempDataSource
    {
        public SqlConnection sqlConnection { get; set; }
        public string tableName { get; set; }
        [NotMapped] private SqlHandler _sqlHandler;

        public TempDataSource(SqlConnection sqlConnection, string tableName, SqlHandler sqlHandler)
        {
            _sqlHandler = sqlHandler;
            this.sqlConnection = sqlConnection;
            this.tableName = tableName;
        }

        public void ExecuteNonQuery(string queryString)
        {
            _sqlHandler.ExecuteNonQuery(sqlConnection, queryString);
        }
    }
}
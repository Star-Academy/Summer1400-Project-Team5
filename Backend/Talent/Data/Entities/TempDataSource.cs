using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Data.SqlClient;
using Talent.Services.Interfaces;

namespace Talent.Data.Entities
{
    public class TempDataSource
    {
        public int Id { get; set; }
        public string tableName { get; set; }
        [NotMapped] public SqlConnection sqlConnection { get; set; }
        [NotMapped] private ISqlHandler _sqlHandler;

        public TempDataSource()
        {
        }

        public TempDataSource(SqlConnection sqlConnection, string tableName, ISqlHandler sqlHandler)
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
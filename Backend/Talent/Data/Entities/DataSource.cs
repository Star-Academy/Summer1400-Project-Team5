using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Data.SqlClient;
using Talent.Models;
using Talent.Models.DatabaseModels;
using Talent.Services.Interfaces;

namespace Talent.Data.Entities
{
    public class DataSource
    {
        [Key] public int Id { get; set; }
        public string TableName { get; set; }
        [NotMapped] private ISqlHandler _sqlHandler;
        [NotMapped] public SqlConnection SqlConnection { get; set; }

        [NotMapped] private const string ClonedTableSuffix = "CLONED";
        [NotMapped] private const string TemporaryTableSuffix = "TEMPORARY";

        public DataSource(ISqlHandler sqlHandler, SqlConnection sqlConnection)
        {
            _sqlHandler = sqlHandler;
            SqlConnection = sqlConnection;
        }

        public DataSource(SqlConnection sqlConnection, string tableName, ISqlHandler sqlHandler)
        {
            SqlConnection = sqlConnection;
            TableName = tableName;
            _sqlHandler = sqlHandler;
        }

        public int ExecuteNonQuery(string queryString)
        {
            return _sqlHandler.ExecuteNonQuery(SqlConnection, queryString);
        }

        public void CloseConnection()
        {
            _sqlHandler.CloseConnection(SqlConnection);
        }

        public void DropTable()
        {
            _sqlHandler.DropTableIfExists(SqlConnection, TableName);
        }

        public TempDataSource CloneTable()
        {
            var clonedTableName = TableName + ClonedTableSuffix;
            var resultTable = new TempDataSource(SqlConnection, clonedTableName, _sqlHandler);
            _sqlHandler.DropTableIfExists(SqlConnection, clonedTableName);
            _sqlHandler.CopyTable(SqlConnection, SqlConnection, TableName, clonedTableName);
            return resultTable;
        }

        public TempDataSource CloneTable(int rowCount)
        {
            var clonedTableName = "##" + TableName + TemporaryTableSuffix;
            var resultTable = new TempDataSource(SqlConnection, clonedTableName, _sqlHandler);
            _sqlHandler.DropTableIfExists(SqlConnection, clonedTableName);
            _sqlHandler.ExecuteNonQuery(SqlConnection, $"SELECT TOP({rowCount}) * INTO" +
                                                       $" {SqlConnection.Database}.dbo.{clonedTableName} " +
                                                       $"FROM {SqlConnection.Database}.dbo.{TableName}");
            return resultTable;
        }
    }
}
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Data.SqlClient;
using Talent.Models;
using Talent.Services.Interfaces;

namespace Talent.Data.Entities
{
    public class DataSource
    {
        public SqlConnection sqlConnection { get; set; }
        public string tableName { get; set; }
        [NotMapped] private ISqlHandler _sqlHandler;

        [NotMapped] private const string ClonedTableSuffix = "CLONED";
        [NotMapped] private const string TemporaryTableSuffix = "TEMPORARY";

        public DataSource(SqlConnection sqlConnection, string tableName, ISqlHandler sqlHandler)
        {
            this.sqlConnection = sqlConnection;
            this.tableName = tableName;
            _sqlHandler = sqlHandler;
        }

        public int ExecuteNonQuery(string queryString)
        {
            return _sqlHandler.ExecuteNonQuery(sqlConnection, queryString);
        }

        public void CloseConnection()
        {
            _sqlHandler.CloseConnection(sqlConnection);
        }

        public void DropTable()
        {
            _sqlHandler.DropTableIfExists(sqlConnection, tableName);
        }

        public TempDataSource CloneTable()
        {
            var clonedTableName = tableName + ClonedTableSuffix;
            var resultTable = new TempDataSource(sqlConnection, clonedTableName, _sqlHandler);
            _sqlHandler.DropTableIfExists(sqlConnection, clonedTableName);
            _sqlHandler.CopyTable(sqlConnection, sqlConnection, tableName, clonedTableName);
            return resultTable;
        }

        public TempDataSource CloneTable(int rowCount)
        {
            var clonedTableName = "##" + tableName + TemporaryTableSuffix;
            var resultTable = new TempDataSource(sqlConnection, clonedTableName, _sqlHandler);
            _sqlHandler.DropTableIfExists(sqlConnection, clonedTableName);
            _sqlHandler.ExecuteNonQuery(sqlConnection, $"SELECT TOP({rowCount}) * INTO" +
                                                       $" {sqlConnection.Database}.dbo.{clonedTableName} " +
                                                       $"FROM {sqlConnection.Database}.dbo.{tableName}");
            return resultTable;
        }
    }
}
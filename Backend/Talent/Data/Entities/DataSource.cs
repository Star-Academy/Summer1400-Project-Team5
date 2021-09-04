using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using Talent.Models;
using Talent.Services.Interfaces;

namespace Talent.Data.Entities
{
    public class DataSource
    {
        public SqlConnection sqlConnection { get; set; }
        public string tableName { get; set; }
        [NotMapped] private SqlHandler _sqlHandler;

        [NotMapped] private const string ClonedTableSuffix = "CLONED";
        [NotMapped] private const string TemporaryTableSuffix = "TEMPORARY";

        public DataSource(SqlConnection sqlConnection, string tableName, SqlHandler sqlHandler)
        {
            this.sqlConnection = sqlConnection;
            this.tableName = tableName;
            _sqlHandler = sqlHandler;
        }

        private int ExecuteNonQuery(string queryString)
        {
            CheckConnection();
            try
            {
                sqlConnection.Open();
                var command = new SqlCommand(queryString, sqlConnection);
                var result = command.ExecuteNonQuery();
                return result;
            }
            catch
            {
                throw new Exception("Cannot open DataSource connection.");
            }
            finally
            {
                CheckConnection();
            }
        }

        private void CheckConnection()
        {
            if (_sqlHandler.IsOpen(sqlConnection))
                sqlConnection.Close();
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
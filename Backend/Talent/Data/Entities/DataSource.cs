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
        public string DatabaseName { get; set; }

        [NotMapped] private const string ClonedTableSuffix = "CLONED";
        [NotMapped] private const string TemporaryTableSuffix = "TEMPORARY";

        public DataSource()
        {
        }

        public DataSource(string tableName, string databaseName)
        {
            TableName = tableName;
            DatabaseName = databaseName;
        }

        public TempDataSource CloneTable(ISqlHandler sqlHandler)
        {
            var clonedTableName = "##" + TableName + ClonedTableSuffix;
            var resultTable = new TempDataSource(clonedTableName, DatabaseName);
            sqlHandler.DropTableIfExists(clonedTableName);
            sqlHandler.CopyTable(DatabaseName, DatabaseName, TableName, clonedTableName);
            return resultTable;
        }

        public TempDataSource CloneTable(int rowCount, ISqlHandler sqlHandler)
        {
            var clonedTableName = "##" + TableName + TemporaryTableSuffix;
            var resultTable = new TempDataSource(clonedTableName, DatabaseName);
            sqlHandler.DropTableIfExists(clonedTableName);
            sqlHandler.ExecuteNonQuery($"SELECT TOP({rowCount}) * INTO" +
                                                       $" {DatabaseName}.dbo.{clonedTableName} " +
                                                       $"FROM {DatabaseName}.dbo.{TableName}");
            return resultTable;
        }
    }
}
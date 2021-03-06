using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Talent.Services.Interfaces;

namespace Talent.Data.Entities
{
    public class DataSource
    {
        [Key] public int Id { get; set; }
        public string TableName { get; set; }
        public string DatabaseName { get; set; }

        [NotMapped] private const string ClonedTableSuffix = "Cloned1";
        [NotMapped] private const string TemporaryTableSuffix = "Temporary1";

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
            var clonedTableName = TableName + ClonedTableSuffix;
            var resultTable = new TempDataSource(clonedTableName, DatabaseName);
            sqlHandler.DropTableIfExists(clonedTableName);
            sqlHandler.CopyTable(DatabaseName, DatabaseName, TableName, clonedTableName);
            return resultTable;
        }

        public TempDataSource CloneTable(int rowCount, ISqlHandler sqlHandler)
        {
            var clonedTableName = TableName + TemporaryTableSuffix;
            var resultTable = new TempDataSource(clonedTableName, DatabaseName);
            sqlHandler.DropTableIfExists(clonedTableName);
            sqlHandler.ExecuteNonQuery($"SELECT TOP({rowCount}) * INTO" +
                                                       $" {DatabaseName}.dbo.{clonedTableName} " +
                                                       $"FROM {DatabaseName}.dbo.{TableName}");
            return resultTable;
        }
    }
}
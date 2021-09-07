
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Talent.Services.Interfaces;

namespace Talent.Data.Entities
{
    public class TempDataSource
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public string DatabaseName { get; set; }

        [NotMapped] private const string ClonedTableSuffix = "Cloned";
        [NotMapped] private const string TemporaryTableSuffix = "Temporary";

        public TempDataSource()
        {
        }

        public TempDataSource(string tableName, string databaseName)
        {
            TableName = tableName;
            DatabaseName = databaseName;
        }

        public TempDataSource CloneTable(ISqlHandler sqlHandler)
        {
            var clonedTableName = FindNextName(TableName, ClonedTableSuffix);
            var resultTable = new TempDataSource(clonedTableName, DatabaseName);
            sqlHandler.DropTableIfExists(clonedTableName);
            sqlHandler.CopyTable(DatabaseName, DatabaseName, TableName, clonedTableName);
            return resultTable;
        }

        public TempDataSource CloneTable(int rowCount, ISqlHandler sqlHandler)
        {
            var clonedTableName = FindNextName(TableName, TemporaryTableSuffix);
            var resultTable = new TempDataSource(clonedTableName, DatabaseName);
            sqlHandler.DropTableIfExists(clonedTableName);
            sqlHandler.ExecuteNonQuery($"SELECT TOP({rowCount}) * INTO" +
                                       $" {DatabaseName}.dbo.{clonedTableName} " +
                                       $"FROM {DatabaseName}.dbo.{TableName}");
            return resultTable;
        }

        private string FindNextName(string name, string splitter)
        {
            try
            {
                var tableNameParts = name.Split(splitter);
                tableNameParts[^1] = splitter + (int.Parse(tableNameParts[^1]) + 1);
                return tableNameParts.Aggregate("", (current, s) => current + s);
            }
            catch
            {
                return name + splitter;
            }
        }
    }
}
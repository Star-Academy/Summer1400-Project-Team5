using System.Data;
using Talent.Services.Interfaces;

namespace Talent.Models.DatabaseModels
{
    public class SqlTable : ISqlTable
    { 
        public string GetCreatTableQuery(DataTable table)
        {
            var queryString = $"CREATE TABLE {table.TableName} {GetColumnListString(table)};";
            return queryString;
        }

        private string GetColumnListString(DataTable table)
        {
            var query = "(";
            for (var i = 0; i < table.Columns.Count; i++)
            {
                query += "\n [" + table.Columns[i].ColumnName + "] ";
                var columnType = table.Columns[i].DataType.ToString();
                query += GetTypeInSql(table, columnType, i);
                if (table.Columns[i].AutoIncrement)
                    query += " IDENTITY(" + table.Columns[i].AutoIncrementSeed + "," + table.Columns[i].AutoIncrementStep + ") ";
                if (!table.Columns[i].AllowDBNull)
                    query += " NOT NULL ";
                query += ",";
            }

            return query.Substring(0,query.Length-1) + "\n)";
        }

        private string GetTypeInSql(DataTable table, string columnType, int columnNumber)
        {
            var sqlType = columnType switch
            {
                "System.Int32" => " int ",
                "System.Int64" => " bigint ",
                "System.Int16" => " smallint",
                "System.Byte" => " tinyint",
                "System.Decimal" => " decimal ",
                "System.DateTime" => " datetime ",
                "system.double" => " float ",
                _ =>
                    $" nvarchar({(table.Columns[columnNumber].MaxLength == -1 ? "max" : table.Columns[columnNumber].MaxLength.ToString())}) "
            };

            return sqlType;
        }
    }
}
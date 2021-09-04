using System.Collections.Generic;
using System.Data;
using System.IO;
using Microsoft.Data.SqlClient;
using Talent.Models;
using Talent.Services.Interfaces;

namespace Talent.Services.Parsers
{
    public class CsvParser : ICsvParser
    {
        private readonly ISqlHandler _sqlHandler;

        public CsvParser(ISqlHandler sqlHandler)
        {
            _sqlHandler = sqlHandler;
        }

        public void ConvertCsvToSql(SqlConnection connection, string tableName, CsvFile csvFile)
        {
            var dataTable = ConvertCsvToDataTable(csvFile);
            dataTable.TableName = tableName;
            ConvertDataTableToSql(connection, dataTable);

        }

        private void ConvertDataTableToSql(SqlConnection connection, DataTable dataTable)
        {
            if (!_sqlHandler.IsOpen(connection))
            {
                connection.Open();
            }
            _sqlHandler.DropTableIfExists(connection, dataTable.TableName);
            var query = CreateTable(dataTable.TableName, dataTable);
            var sqlCommand = new SqlCommand(query, connection);
            sqlCommand.ExecuteNonQuery();
            using var sqlBulkCopy = new SqlBulkCopy(connection);
            sqlBulkCopy.DestinationTableName = dataTable.TableName;
            sqlBulkCopy.WriteToServer(dataTable);
        }

        private DataTable ConvertCsvToDataTable(CsvFile csvFile)
        {
            var dataTable = new DataTable();
            using var streamReader = new StreamReader(csvFile.FormFile.OpenReadStream());
            var firstRow = streamReader.ReadLine().Split(csvFile.Delimiter);
            var headers = ExtractHeaders(firstRow, csvFile.HasHeader);
            AddColumns(dataTable, headers);
            while (!streamReader.EndOfStream)
            {
                var rows = streamReader.ReadLine().Split(csvFile.Delimiter);
                AddRows(dataTable, rows, firstRow.Length);
            }
            return dataTable;
        }
        
        private string[] ExtractHeaders(string[] firstRow, bool hasHeader)
        {
            var headers = new List<string>();
            if (hasHeader)
            {
                return firstRow;
            }
            for (var i = 0; i < firstRow.Length; i++)
            {
                headers.Add($"field-{i}");
            }
            return headers.ToArray();
        }

        private void AddColumns(DataTable dataTable, string[] headers)
        {
            foreach (var header in headers)
            {
                dataTable.Columns.Add(header);
            }
        }

        private void AddRows(DataTable dataTable, string[] rows, int rowLength)
        {
            var dataRow = dataTable.NewRow();
            for (var i = 0; i < rowLength; i++)
            {
                dataRow[i] = rows[i];
            }
            dataTable.Rows.Add(dataRow);
        }

        private string CreateTable(string tableName, DataTable table)
        {
            var query = "CREATE TABLE " + tableName + "(";
            for (var i = 0; i < table.Columns.Count; i++)
            {
                query += "\n [" + table.Columns[i].ColumnName + "] ";
                var columnType = table.Columns[i].DataType.ToString();
                query += columnType switch
                {
                    "System.Int32" => " int ",
                    "System.Int64" => " bigint ",
                    "System.Int16" => " smallint",
                    "System.Byte" => " tinyint",
                    "System.Decimal" => " decimal ",
                    "System.DateTime" => " datetime ",
                    _ =>
                        $" nvarchar({(table.Columns[i].MaxLength == -1 ? "max" : table.Columns[i].MaxLength.ToString())}) "
                };
                if (table.Columns[i].AutoIncrement)
                    query += " IDENTITY(" + table.Columns[i].AutoIncrementSeed + "," + table.Columns[i].AutoIncrementStep + ") ";
                if (!table.Columns[i].AllowDBNull)
                    query += " NOT NULL ";
                query += ",";
            }
            return query.Substring(0,query.Length-1) + "\n)";
        }
    }
}
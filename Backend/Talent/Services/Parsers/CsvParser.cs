using System.Data;
using Microsoft.Data.SqlClient;
using Talent.Data.Entities;
using Talent.Models;
using Talent.Services.Interfaces;

namespace Talent.Services.Parsers
{
    public class CsvParser : ICsvParser
    {
        private readonly ISqlHandler _sqlHandler;
        private readonly ICsvToTable _csvToTable;
        private readonly ISqlTable _sqlTable;

        public CsvParser(ISqlHandler sqlHandler, ICsvToTable csvToTable, ISqlTable sqlTable)
        {
            _sqlHandler = sqlHandler;
            _csvToTable = csvToTable;
            _sqlTable = sqlTable;
        }

        public DataSource ConvertCsvToSql(SqlConnection connection, string tableName, CsvFile csvFile)
        {
            var dataTable = _csvToTable.ConvertCsvToDataTable(csvFile);
            dataTable.TableName = tableName;
            ConvertDataTableToSql(connection, dataTable);
            return new DataSource(connection, tableName, (SqlHandler) _sqlHandler);
        }

        private void ConvertDataTableToSql(SqlConnection connection, DataTable dataTable)
        {
            _sqlHandler.DropTableIfExists(connection, dataTable.TableName);
            var query = _sqlTable.GetCreatTableQuery(dataTable);
            _sqlHandler.ExecuteNonQuery(connection, query);
            using var sqlBulkCopy = new SqlBulkCopy(connection);
            sqlBulkCopy.DestinationTableName = dataTable.TableName;
            sqlBulkCopy.WriteToServer(dataTable);
        }
    }
}
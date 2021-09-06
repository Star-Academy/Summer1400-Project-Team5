using System.Data;
using Microsoft.Data.SqlClient;
using Talent.Data.Entities;
using Talent.Models;
using Talent.Services.Interfaces;

namespace Talent.Services.Parsers
{
    public class CsvParser : ICsvParser
    {
        private readonly SqlHandler _sqlHandler;
        private readonly ICsvToTable _csvToTable;
        private readonly ISqlTable _sqlTable;

        public CsvParser(SqlHandler sqlHandler, ICsvToTable csvToTable, ISqlTable sqlTable)
        {
            _sqlHandler = sqlHandler;
            _csvToTable = csvToTable;
            _sqlTable = sqlTable;
        }

        public DataSource ConvertCsvToSql(string tableName, CsvFile csvFile)
        {
            var dataTable = _csvToTable.ConvertCsvToDataTable(csvFile);
            dataTable.TableName = tableName;
            ConvertDataTableToSql(dataTable);
            return new DataSource(tableName, _sqlHandler.Connection.Database);
        }

        private void ConvertDataTableToSql(DataTable dataTable)
        {
            _sqlHandler.DropTableIfExists(dataTable.TableName);
            var query = _sqlTable.GetCreatTableQuery(dataTable);
            _sqlHandler.ExecuteNonQuery(query);
            using var sqlBulkCopy = new SqlBulkCopy(_sqlHandler.Connection);
            sqlBulkCopy.DestinationTableName = dataTable.TableName;
            sqlBulkCopy.WriteToServer(dataTable);
        }
    }
}
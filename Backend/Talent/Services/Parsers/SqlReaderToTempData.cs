using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Talent.Data.Entities;
using Talent.Services.Interfaces;

namespace Talent.Services.Parsers
{
    public class SqlReaderToTempData
    {
        private ISqlTable _sqlTable;
        private DataReaderToDataTable _dataReader;

        public SqlReaderToTempData(ISqlTable sqlTable, DataReaderToDataTable dataReader)
        {
            _sqlTable = sqlTable;
            _dataReader = dataReader;
        }

        public TempDataSource ConvertToTempDataSource(SqlDataReader dataReader, ISqlHandler sqlHandler, string tableName)
        {
            var dataTable = _dataReader.ConvertToDataTable(dataReader, tableName);
            CopyToDatabase(dataTable, sqlHandler);
            var resultTemp = new TempDataSource()
            {
                TableName = dataTable.TableName,
                DatabaseName = sqlHandler.Connection.Database
            };
            return resultTemp;
        }

        private void CopyToDatabase(DataTable dataTable, ISqlHandler sqlHandler)
        {
            sqlHandler.DropTableIfExists(dataTable.TableName);
            var query = _sqlTable.GetCreatTableQuery(dataTable);
            sqlHandler.ExecuteNonQuery(query);
            sqlHandler.OpenConnection();
            using var sqlBulkCopy = new SqlBulkCopy(sqlHandler.Connection);
            sqlBulkCopy.DestinationTableName = dataTable.TableName;
            sqlBulkCopy.WriteToServer(dataTable);
            sqlHandler.CloseConnection();
        }
    }
}
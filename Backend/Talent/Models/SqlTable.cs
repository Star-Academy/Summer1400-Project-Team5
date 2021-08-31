using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Talent.Models
{
    public class SqlTable : ITable
    {
        private SqlConnection _connection;
        private string _tableName;

        public SqlTable(SqlConnection connection, string tableName)
        {
            _connection = connection;
            _tableName = tableName;
        }

        public void LoadTableFromExistingOne(SqlConnection oldTableConnection, string oldTableName)
        {
            DropTable(_connection, _tableName);
            var queryString = @$"SELECT * INTO {_connection.Database}.dbo.{_tableName} FROM 
                                {oldTableConnection.Database}.dbo.{oldTableName}";
            var sqlCommand = new SqlCommand(queryString, _connection);
            sqlCommand.ExecuteNonQuery();
        }

        private void DropTable(SqlConnection tableConnection, string tableName)
        {
            var queryString = "" +
                              @$"IF OBJECT_ID('{tableConnection.Database}.dbo.{tableName}') IS NOT NULL
                               BEGIN
                                  DROP TABLE {tableName};
                               END;";
            var sqlCommand = new SqlCommand(queryString, tableConnection);
            sqlCommand.ExecuteNonQuery();
        }
    }
}
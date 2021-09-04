using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Talent.Services.Interfaces;

namespace Talent.Models
{
    public class SqlHandler : ISqlHandler
    {
        public void DropTableIfExists(SqlConnection connection, string tableName)
        {
            var queryString = $"DROP TABLE IF EXISTS {tableName}";
            ExecuteNonQuery(connection, queryString);
        }

        public bool IsOpen(SqlConnection connection)
        {
            return connection.State == ConnectionState.Open;
        }

        public int ExecuteNonQuery(SqlConnection connection, string queryString)
        {
            CheckConnection(connection);
            try
            {
                connection.Open();
                var command = new SqlCommand(queryString, connection);
                return command.ExecuteNonQuery();
            }
            catch
            {
                throw new Exception("Cannot open sql connection.");
            }
            finally
            {
                CheckConnection(connection);
            }
        }

        private void CheckConnection(SqlConnection connection)
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
    }
}
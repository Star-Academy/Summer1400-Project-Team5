using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Talent.Services.Interfaces;

namespace Talent.Models
{
    public class SqlHandler : ISqlHandler
    {
        public bool IsOpen(SqlConnection connection)
        {
            return connection.State == ConnectionState.Open;
        }

        public int ExecuteNonQuery(SqlConnection connection, string queryString)
        {
            CloseConnection(connection);
            try
            {
                connection.Open();
                var command = new SqlCommand(queryString, connection);
                return command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("Cannot open sql connection.");
            }
            finally
            {
                CloseConnection(connection);
            }
        }
        
        public SqlDataReader ExecuteReader(SqlConnection connection, string queryString)
        {
            CloseConnection(connection);
            try
            {
                connection.Open();
                var command = new SqlCommand(queryString, connection);
                return command.ExecuteReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("Cannot open sql connection.");
            }
        }

        public void CloseConnection(SqlConnection connection)
        {
            if (IsOpen(connection))
                connection.Close();
        }

        public void DropTableIfExists(SqlConnection connection, string tableName)
        {
            var queryString = $"DROP TABLE IF EXISTS {tableName}";
            ExecuteNonQuery(connection, queryString);
        }

        public void CopyTable(SqlConnection sourceConnection,
            SqlConnection destinationConnection,
            string sourceTableName,
            string destinationTableName)
        {
            ExecuteNonQuery(sourceConnection,
                $"SELECT * INTO {destinationConnection.Database}.dbo.{destinationTableName} " +
                            $"FROM {sourceConnection.Database}.dbo.{sourceTableName}");
        }
    }
}
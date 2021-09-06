using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Talent.Services.Interfaces;

namespace Talent.Models
{
    public class SqlHandler : ISqlHandler
    {
        public SqlConnection Connection { get; }

        public SqlHandler(SqlConnection sqlConnection)
        {
            Connection = sqlConnection;
        }

        public bool IsOpen()
        {
            return Connection.State == ConnectionState.Open;
        }

        public void CloseConnection()
        {
            if (IsOpen())
                Connection.Close();
        }

        public int ExecuteNonQuery(string queryString)
        {
            CloseConnection();
            try
            {
                Connection.Open();
                var command = new SqlCommand(queryString, Connection);
                var result = command.ExecuteNonQuery();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("Cannot open sql connection in ExecuteNonQuery method.");
            }
            finally
            {
                CloseConnection();
            }
        }

        public SqlDataReader ExecuteReader(string queryString)
        {
            CloseConnection();
            try
            {
                Connection.Open();
                var command = new SqlCommand(queryString, Connection);
                var result = command.ExecuteReader();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("Cannot open sql connection.");
            }
            finally
            {
                CloseConnection();
            }
        }

        public void DropTableIfExists(string tableName)
        {
            var queryString = $"DROP TABLE IF EXISTS {tableName}";
            ExecuteNonQuery(queryString);
        }

        public void CopyTable(string sourceDatabaseName,
            string destinationDatabaseName,
            string sourceTableName,
            string destinationTableName)
        {
            ExecuteNonQuery($"SELECT * INTO {destinationDatabaseName}.dbo.{destinationTableName} " +
                            $"FROM {sourceDatabaseName}.dbo.{sourceTableName}");
        }
    }
}
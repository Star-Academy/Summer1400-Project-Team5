﻿using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Talent.Data.Entities;
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
                $"SELECT * INTO {destinationConnection}.dbo.{destinationTableName} " +
                            $"FROM {sourceConnection}.dbo.{sourceTableName}");
        }
    }
}
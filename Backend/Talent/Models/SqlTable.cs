using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Talent.Models
{
    public class SqlTable : ITable
    {
        private List<string> _columnNames;
        private SqlConnection _connection;
        private string _tableName;

        public SqlTable(string connectionString, string tableName)
        {
            _columnNames = new List<string>();
            try
            {
                _connection = new SqlConnection(connectionString);
                _connection.Open();
                _tableName = tableName;
            }
            catch
            {
                throw new Exception("Cannot connect to the database server.");
            }
            InitializeColumnNames();
        }

        private void InitializeColumnNames()
        {
            var queryString = $"SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('{_tableName}');";
            var command = new SqlCommand(queryString, _connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
                _columnNames.Add(reader.GetString("name"));
            reader.Close();
        }

        public ITable CloneTable()
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;

namespace Talent.Models.DatabaseModels
{
    public class DbTable : DataTable
    {
        private readonly SqlConnection _sqlConnection;

        private const int VarCharSize = 50;

        public DbTable(SqlConnection sqlConnection, string? tableName) : base(tableName)
        {
            _sqlConnection = sqlConnection;
            if (_sqlConnection.State == ConnectionState.Closed)
            {
                try
                {
                    _sqlConnection.Open();
                }
                catch
                {
                    throw new Exception("Cannot open the sql connection.");
                }
            }
        }

        public int ExecuteNonQuery(string queryString)
        {
            var command = new SqlCommand(queryString, _sqlConnection);
            return command.ExecuteNonQuery();
        }

        public void CreateTableInDatabase()
        {
            var queryString = $"CREATE TABLE {TableName} {GetColumnListString()};";
            ExecuteNonQuery(queryString);
        }

        private string GetColumnListString()
        {
            var resultString = "";
            foreach (DataColumn column in Columns)
                resultString += column.ColumnName + " " + GetTypeInSql(column.GetType()) + ",\n";
            return $"(\n{resultString})";
        }

        private string GetTypeInSql(Type dataType)
        {
            if (dataType == typeof(int))
                return "INT";
            if (dataType == typeof(long))
                return "BIGINT";
            if (dataType == typeof(double))
                return "FLOAT";
            return $"VARCHAR({VarCharSize})";
        }

        protected override void OnTableNewRow(DataTableNewRowEventArgs e)
        {
            base.OnTableNewRow(e);
            var newRow = e.Row;
            ExecuteNonQuery(GetInsertQueryString(newRow));
        }

        private string GetInsertQueryString(DataRow row)
        {
            return $"INSERT INTO {TableName} {GetColumnListString()} VALUES {GetValuesListString(row)};"
        }

        private string GetValuesListString(DataRow row)
        {
            var resultString = "";
            foreach (DataColumn column in Columns)
                resultString += row[column.ColumnName] + ", ";
            return $"({resultString})"
        }
    }
}
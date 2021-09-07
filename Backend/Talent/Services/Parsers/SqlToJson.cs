using System.Data;
using Newtonsoft.Json;
using Talent.Services.Interfaces;

namespace Talent.Services.Parsers
{
    public class SqlToJson : ISqlToJson
    {
        private readonly ISqlHandler _sqlHandler;

        public SqlToJson(ISqlHandler sqlHandler)
        {
            _sqlHandler = sqlHandler;
        }

        public string ConvertSqlTableToJson(string tableName, int rowCount)
        {
            var query = $"SELECT TOP {rowCount} * FROM {tableName}";
            return SqlTableToJson(query);
        }

        public string ConvertSqlTableToJson(string tableName)
        {
            var query = $"SELECT * FROM {tableName}";
            return SqlTableToJson(query);
        }

        private string SqlTableToJson(string query)
        {
            var table = SqlToDataTable(query);
            var jsonString = DataTableToJson(table);
            return jsonString; 
        }
        
        private string DataTableToJson(DataTable table)
        {
            var jsonString = JsonConvert.SerializeObject(table);
            return jsonString;
        }

        private DataTable SqlToDataTable(string query)
        {
            var dataAdapter = _sqlHandler.CreateDataAdapter(query);
            var table = new DataTable();
            dataAdapter.Fill(table);
            return table;
        }
    }
}
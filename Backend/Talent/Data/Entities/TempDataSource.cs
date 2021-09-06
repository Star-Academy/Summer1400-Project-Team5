using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Data.SqlClient;
using Talent.Services.Interfaces;

namespace Talent.Data.Entities
{
    public class TempDataSource
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public string DatabaseName { get; set; }

        public TempDataSource()
        {
        }

        public TempDataSource(string tableName, string databaseName)
        {
            TableName = tableName;
            DatabaseName = databaseName;
        }
    }
}
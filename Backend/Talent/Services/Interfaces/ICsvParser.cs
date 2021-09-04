using Microsoft.Data.SqlClient;
using Talent.Data.Entities;
using Talent.Models;

namespace Talent.Services.Interfaces
{
    public interface ICsvParser
    {
        DataSource ConvertCsvToSql(SqlConnection connection, string tableName, CsvFile csvFile);
    }
}
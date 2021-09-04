using Microsoft.Data.SqlClient;
using Talent.Models;

namespace Talent.Services.Interfaces
{
    public interface ICsvParser
    {
        void ConvertCsvToSql(SqlConnection connection, string tableName, CsvFile csvFile);
    }
}
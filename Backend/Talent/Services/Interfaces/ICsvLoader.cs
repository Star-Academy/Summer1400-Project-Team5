using Microsoft.Data.SqlClient;
using Talent.Models;

namespace Talent.Services.Interfaces
{
    public interface ICsvLoader
    {
        void LoadCsv(SqlConnection connection, string tableName, CsvFile csvFile);
    }
}
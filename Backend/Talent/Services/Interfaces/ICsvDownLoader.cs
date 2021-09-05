using Microsoft.Data.SqlClient;
using Talent.Models;

namespace Talent.Services.Interfaces
{
    public interface ICsvDownloader
    {
        string DownloadCsv(SqlConnection connection, string tableName, CsvFile csvFile);
    }
}
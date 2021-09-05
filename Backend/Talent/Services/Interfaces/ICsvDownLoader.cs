using Microsoft.Data.SqlClient;
using Talent.Models;

namespace Talent.Services.Interfaces
{
    public interface ICsvDownloader
    {
        void DownloadCsv(SqlConnection connection, string tableName, CsvFile csvFile);
    }
}
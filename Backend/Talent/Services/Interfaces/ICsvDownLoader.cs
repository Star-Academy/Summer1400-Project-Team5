using Talent.Models;

namespace Talent.Services.Interfaces
{
    public interface ICsvDownloader
    {
        string DownloadCsv(string tableName, CsvFile csvFile);
    }
}
using System.Data;
using Talent.Models;

namespace Talent.Services.Interfaces
{
    public interface ICsvToTable
    {
        DataTable ConvertCsvToDataTable(CsvFile csvFile);
    }
}
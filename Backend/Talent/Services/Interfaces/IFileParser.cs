using Microsoft.Data.SqlClient;
using Talent.Models;

namespace Talent.Services.Interfaces
{
    public interface IFileParser
    {
        public ITable ConvertToTable(SqlConnection connectionString, string tableName);
    }
}
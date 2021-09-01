using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;

namespace Talent.Services.Interfaces
{
    public interface IParser
    {
        public Table ConvertToTable(SqlConnection connectionString, string tableName);
    }
}
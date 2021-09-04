using Microsoft.Data.SqlClient;

namespace Talent.Services.Interfaces
{
    public interface ISqlParser
    {
        void CloneTable(SqlConnection srcConnection, SqlConnection destConnection, string srcName, string destName);
    }
}
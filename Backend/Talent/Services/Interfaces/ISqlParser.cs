using Microsoft.Data.SqlClient;
using Talent.Data.Entities;

namespace Talent.Services.Interfaces
{
    public interface ISqlParser
    {
        DataSource CloneTable(SqlConnection srcConnection, SqlConnection destConnection, string srcName, string destName);
    }
}
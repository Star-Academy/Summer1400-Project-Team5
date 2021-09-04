using Microsoft.Data.SqlClient;

namespace Talent.Services.Interfaces
{
    public interface ISqlHandler
    {
        void DropTableIfExists(SqlConnection connection, string tableName);
        bool IsOpen(SqlConnection connection);
    }
}
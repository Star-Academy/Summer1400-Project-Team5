using Microsoft.Data.SqlClient;

namespace Talent.Services.Interfaces
{
    public interface ISqlHandler
    {
        void DropTableIfExists(SqlConnection connection, string tableName);
        bool IsOpen(SqlConnection connection);
        int ExecuteNonQuery(SqlConnection connection, string queryString);
        SqlDataReader ExecuteReader(SqlConnection connection, string queryString);

        void CopyTable(SqlConnection sourceConnection, SqlConnection destinationConnection, string sourceTableName, string destinationTableName);

        void CloseConnection(SqlConnection connection);
        void OpenConnection(SqlConnection connection);
    }
}
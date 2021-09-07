using Microsoft.Data.SqlClient;

namespace Talent.Services.Interfaces
{
    public interface ISqlHandler
    {
        SqlConnection Connection { get; }
        
        void CloseConnection();

        void DropTableIfExists(string tableName);

        bool IsOpen();

        int ExecuteNonQuery(string queryString);

        SqlDataReader ExecuteReader(string queryString);
        
        void CopyTable(string sourceDatabaseName, string destinationDatabaseName, string sourceTableName, string destinationTableName);

        void OpenConnection();

        SqlDataAdapter CreateDataAdapter(string queryString);
    }
}
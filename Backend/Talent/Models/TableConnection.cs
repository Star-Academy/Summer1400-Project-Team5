namespace Talent.Models
{
    public class TableConnection
    {
        public ConnectionString connectionString { get; }
        public string tableName { get; }

        public TableConnection(ConnectionString connectionString, string tableName)
        {
            this.connectionString = connectionString;
            this.tableName = tableName;
        }
    }
}
namespace Talent.Models
{
    public class ConnectionString
    {
        public string serverName { get; }
        public string databaseName { get; }
        public string tableName { get; }
        public string username { get; }
        public string password { get; }

        public ConnectionString(string serverName, string databaseName, string tableName, string username, string password)
        {
            this.serverName = serverName;
            this.databaseName = databaseName;
            this.tableName = tableName;
            this.username = username;
            this.password = password;
        }
    }
}
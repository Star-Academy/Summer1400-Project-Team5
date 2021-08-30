namespace Talent.Models
{
    public class ConnectionString
    {
        public string serverName { get; }
        public string databaseName { get; }
        public string username { get; }
        public string password { get; }

        public ConnectionString(string serverName, string databaseName, string username, string password)
        {
            this.serverName = serverName;
            this.databaseName = databaseName;
            this.username = username;
            this.password = password;
        }

        public override string ToString()
        {
            return
                $"Server={serverName};Database={databaseName};Trusted_Connection=False;User={username};Password={password}";
        }
    }
}
namespace Talent.Models
{
    public class ConnectionString
    {
        public string ServerName { get; }
        public string DatabaseName { get; }
        public string Username { get; }
        public string Password { get; }

        public ConnectionString(string serverName, string databaseName, string username, string password)
        {
            this.ServerName = serverName;
            this.DatabaseName = databaseName;
            this.Username = username;
            this.Password = password;
        }

        public override string ToString()
        {
            return
                $"Server={ServerName};Database={DatabaseName};Trusted_Connection=False;User={Username};Password={Password}";
        }
    }
}
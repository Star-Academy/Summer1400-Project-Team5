namespace Talent.Models.DatabaseModels
{
    public class TableConnection
    {
        public ConnectionString ConnectionString { get; }
        public string SourceTable { get; }
        public string DestTable { get; }

        public TableConnection(ConnectionString connectionString, string sourceTable, string destTable)
        {
            ConnectionString = connectionString;
            SourceTable = sourceTable;
            DestTable = destTable;
        }
    }
}
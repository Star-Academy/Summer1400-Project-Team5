using System.Data;

namespace Talent.Models.DatabaseModels
{
    public class DbTable : DataTable
    {
        public DbTable(string? tableName) : base(tableName)
        {
        }


    }
}
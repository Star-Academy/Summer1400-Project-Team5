using Microsoft.SqlServer.Management.Smo;

namespace Talent.Services.Interfaces
{
    public interface IParser
    {
        public Table ConvertToTable(Database database, string tableName);
    }
}
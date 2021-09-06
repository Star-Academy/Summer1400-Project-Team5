using Microsoft.Data.SqlClient;
using Talent.Data.Entities;

namespace Talent.Services.Interfaces
{
    public interface ISqlParser
    {
        DataSource CloneTable(string srcDatabaseName, string destDatabaseName, string srcName, string destName);
    }
}
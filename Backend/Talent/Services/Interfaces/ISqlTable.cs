using System.Data;

namespace Talent.Services.Interfaces
{
    public interface ISqlTable
    {
        string GetCreatTableQuery(DataTable table);
    }
}
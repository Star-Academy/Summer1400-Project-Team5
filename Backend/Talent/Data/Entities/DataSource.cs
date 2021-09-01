using Microsoft.SqlServer.Management.Smo;
using Talent.Models;

namespace Talent.Data.Entities
{
    public class DataSource
    {
        public Table dataSource { get; set; }

        public DataSource(Table dataSource)
        {
            this.dataSource = dataSource;
        }
    }
}
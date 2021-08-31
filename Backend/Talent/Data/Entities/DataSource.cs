using Talent.Models;

namespace Talent.Data.Entities
{
    public class DataSource
    {
        public ITable dataSource { get; set; }

        public DataSource(ITable dataSource)
        {
            this.dataSource = dataSource;
        }
    }
}
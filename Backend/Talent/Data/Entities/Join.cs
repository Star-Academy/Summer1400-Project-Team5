using System.ComponentModel.DataAnnotations.Schema;
using Talent.Models;
using Talent.Services.Interfaces;

namespace Talent.Data.Entities
{
    public class Join : Processor
    {
        public JoinMethod JoinMethod { get; set; }
        public string SourceKey { get; set; }
        public string AddSourceKey { get; set; }
        [ForeignKey("AddSource")]
        public int AddSourceId { get; set; }
        public DataSource AddSource { get; set; }

        public new TempDataSource Process(TempDataSource source, ISqlHandler sqlHandler)
        {
            throw new System.NotImplementedException();
        }

        public Join()
        {
        }
    }
}
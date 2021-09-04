using System.ComponentModel.DataAnnotations.Schema;
using Talent.Models;

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
        
        public override DataSource Process(DataSource source)
        {
            throw new System.NotImplementedException();
        }
        
        public override string ToString()
        {
            string answer = base.ToString();
            answer += "\n";
            answer += SourceKey;
            return answer;
        }
    }
}
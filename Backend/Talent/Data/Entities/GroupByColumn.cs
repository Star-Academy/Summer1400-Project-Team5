using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talent.Data.Entities
{
    public class GroupByColumn
    {
        [Key]
        public int GroupByColumnId { get; set; }
        public string ColumnName { get; set; }
        [ForeignKey("Aggregation")]
        public int AggregationId { get; set; }
        public Aggregation Aggregation { get; set; }
    }
}
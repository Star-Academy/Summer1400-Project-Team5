using System.ComponentModel.DataAnnotations;
using Talent.Models;

namespace Talent.Data.Entities
{
    public class Aggregation
    {
        [Key]
        public int AggregationKey { get; set; }

        public AggregationMethod Method { get; set; }
    }
}
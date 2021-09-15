using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Talent.Models;

namespace Talent.Data.Entities
{
    public class Aggregation : Processor
    {
        public AggregationMethod Method { get; set; }
        public List<GroupByColumn> Columns { get; set; }
        public string AggregationColumn { get; set; }
        public override void Process(TempDataSource source)
        {
            throw new System.NotImplementedException();
        }
    }
}
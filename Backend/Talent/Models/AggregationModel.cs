using System.Collections.Generic;

namespace Talent.Models
{
    public enum AggregationMethod
    {
        SUM, COUNT, AVG, MIN, MAX
    }
    public class AggregationModel : ProcessModel
    {
        public AggregationMethod Method { get; set; }
        public string AggregateColumn { get; set; }
        public IList<string> GroupColumns { get; set; }

    }
}
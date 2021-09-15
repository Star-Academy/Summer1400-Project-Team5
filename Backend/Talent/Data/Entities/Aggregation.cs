using System.Collections.Generic;
using Talent.Models;
using Talent.Services.Interfaces;
using System.Linq;

namespace Talent.Data.Entities
{
    public class Aggregation : Processor
    {
        public AggregationMethod Method { get; set; }
        public List<GroupByColumn> Columns { get; set; }
        public string AggregationColumn { get; set; }
        public override TempDataSource Process(TempDataSource source, ISqlHandler sqlHandler)
        {
            string columns = string.Join(" ", Columns);
            var result = sqlHandler.ExecuteReader(@$"SELECT {Method}({AggregationColumn}) 
                FROM {source.TableName} GROUP BY {columns}");
            
            // TODO: build temp data source
        }
    }
}
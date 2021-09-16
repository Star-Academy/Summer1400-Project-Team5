using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Talent.Models;
using Talent.Services.Interfaces;
using System.Linq;
using Talent.Models.DatabaseModels;
using Talent.Services.Parsers;

namespace Talent.Data.Entities
{
    public class Aggregation : Processor
    {
        public AggregationMethod Method { get; set; }
        public List<GroupByColumn> Columns { get; set; }
        public string AggregationColumn { get; set; }
        public new TempDataSource Process(TempDataSource source, ISqlHandler sqlHandler)
        {
            string columns = string.Join(" ", Columns);
            var result = sqlHandler.ExecuteReader(@$"SELECT {Method}({AggregationColumn}) 
                FROM {source.TableName} GROUP BY {columns}");
            var sqlReader = new SqlReaderToTempData(new SqlTable(), new DataReaderToDataTable());
            return sqlReader.ConvertToTempDataSource(result, sqlHandler, source.FindNextName());
        }

        public Aggregation()
        {
        }
    }
}
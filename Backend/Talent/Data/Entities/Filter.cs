using Talent.Models.DatabaseModels;
using Talent.Services.Interfaces;
using Talent.Services.Parsers;

namespace Talent.Data.Entities
{
    public class Filter : Processor
    {
        public string FilterSQL { get; set; }
        public new TempDataSource Process(TempDataSource source, ISqlHandler sqlHandler)
        {
            var result = sqlHandler.ExecuteReader(@$"SELECT * FROM {source.TableName}
             WHERE {FilterSQL}");
            var sqlReader = new SqlReaderToTempData(new SqlTable(), new DataReaderToDataTable());
            return sqlReader.ConvertToTempDataSource(result, sqlHandler, source.FindNextName());
        }

        public Filter()
        {
        }
    }
}
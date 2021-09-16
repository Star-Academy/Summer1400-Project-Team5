using System.ComponentModel.DataAnnotations.Schema;
using Talent.Models;
using Talent.Models.DatabaseModels;
using Talent.Services.Interfaces;
using Talent.Services.Parsers;

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
            var result = sqlHandler.ExecuteReader(@$"SELECT * FROM {source.TableName}
             {JoinMethod} {source.TableName} ON 
             {source.TableName}.{SourceKey}={AddSource.TableName}.{AddSourceKey}");
            var sqlReader = new SqlReaderToTempData(new SqlTable(), new DataReaderToDataTable());
            return sqlReader.ConvertToTempDataSource(result, sqlHandler, source.FindNextName());
        }

        public Join()
        {
        }
    }
}
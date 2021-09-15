using Talent.Services.Interfaces;

namespace Talent.Data.Entities
{
    public class Filter : Processor
    {
        public string FilterSQL { get; set; }
        public override TempDataSource Process(TempDataSource source, ISqlHandler sqlHandler)
        {
            var result = sqlHandler.ExecuteReader(@$"SELECT * FROM {source.TableName}
             WHERE {FilterSQL}");
            
        }
    }
}
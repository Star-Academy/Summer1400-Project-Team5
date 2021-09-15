using Talent.Services.Interfaces;

namespace Talent.Data.Entities
{
    public class Filter : Processor
    {
        public new TempDataSource Process(TempDataSource source, ISqlHandler sqlHandler)
        {
            throw new System.NotImplementedException();
        }

        public Filter()
        {
        }
    }
}
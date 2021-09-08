namespace Talent.Data.Entities
{
    public interface IProcessor
    {
        DataSource Process(DataSource source);
    }
}
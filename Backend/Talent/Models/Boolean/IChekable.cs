namespace Talent.Models
{
    public interface IChekable
    {
        bool CheckCondition<T>(T o);
    }
}
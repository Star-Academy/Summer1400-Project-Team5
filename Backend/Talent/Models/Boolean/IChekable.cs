namespace Talent.Models.Boolean
{
    public interface IChekable
    {
        bool CheckCondition<T>(T o);
    }
}
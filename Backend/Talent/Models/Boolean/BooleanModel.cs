namespace Talent.Models.Boolean
{
    public abstract class BooleanModel : IChekable
    {
        public abstract bool CheckCondition<T>(T o);
    }
}
using Talent.Models.JsonSettings;

namespace Talent.Models.Boolean
{
    public abstract class BooleanModel : IChekable
    {
        public abstract bool CheckCondition<T>(T o);

        public override string ToString()
        {
            return BooleanDeserializer.Serialize(this);
        }
    }
}
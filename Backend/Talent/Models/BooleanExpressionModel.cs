namespace Talent.Models
{
    public class BooleanExpressionModel<T> : BooleanModel
    {
        public BooleanExpressionModel(string field, T expectedValue)
        {
            Field = field;
            ExpectedValue = expectedValue;
        }

        public string Field { get; set; }
        public T ExpectedValue { get; set; }
    }
}
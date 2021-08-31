namespace Talent.Models
{
    public class BooleanExpressionModel : BooleanModel
    {
        public BooleanExpressionModel(string field, string expectedValue)
        {
            Field = field;
            ExpectedValue = expectedValue;
        }
        
        public string Field { get; set; }
        public string ExpectedValue { get; set; }
        public bool CheckCondition<T>(T o)
        {
            throw new System.NotImplementedException();
        }
    }
}
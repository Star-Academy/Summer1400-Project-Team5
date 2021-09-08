using System;
using System.Collections;
using Talent.Models.Convertors;

namespace Talent.Models.Boolean
{
    public class BooleanExpressionModel : BooleanModel
    {
        public BooleanExpressionModel(string field, string expectedValue)
        {
            Field = field;
            ExpectedValue = expectedValue;
        }

        public BooleanExpressionModel()
        {
            
        }
        
        public string Field { get; set; }
        public string ExpectedValue { get; set; }

        public Operator Operator { get; set; }
        public override bool CheckCondition<T>(T o)
        {
            Type tType = typeof(T);
            if (!DoesTypeContainsField<T>(tType)) return false;
            return CheckFieldValueWithExpectedValue(o, tType);
        }

        private bool CheckFieldValueWithExpectedValue<T>(T o, Type tType)
        {
            var actualValue = GetValueOfField(o, tType);
            var expectedObject = ParseExpectedValue();
            return Compare(expectedObject, actualValue);
        }

        private bool Compare(object? expectedObject, object? actualValue)
        {
            try
            {
                int compareResult = Comparer.Default.Compare( actualValue,expectedObject);
                return DoesResultMatchToOperator(compareResult);
            }
            catch (Exception e)
            {
                return false;
            }
            
        }

        private bool DoesResultMatchToOperator(int compareResult)
        {
            switch (Operator)
            {
                case Operator.E:
                    return compareResult == 0; // objects are equal
                case Operator.NE:
                    return compareResult != 0; // objects are not equal
                case Operator.GE:
                    return compareResult >= 0; // object a is greater equal than b
                case Operator.G:
                    return compareResult > 0; // object a is greater than b
                case Operator.LE:
                    return compareResult <= 0; // object a is less equal than b
                case Operator.L:
                    return compareResult < 0; // object a is less than b
                default:
                    return false;
            }
        }

        private object? ParseExpectedValue()
        {
            if (Parser.Default.TryParse(ExpectedValue,out object value))
            {
                return value;
            }

            return ExpectedValue;
        }

        private object? GetValueOfField<T>(T o, Type tType)
        {
            var prop = tType.GetProperty(Field);
            var field = tType.GetField(Field);
            var propValueString = prop?.GetValue(o);
            var fieldValueString = field?.GetValue(o);
            object? value = propValueString ?? fieldValueString;
            return value;
        }

        private bool DoesTypeContainsField<T>(Type tType)
        {
            var field = tType.GetField(Field);
            var prop = tType.GetProperty(Field);
            if (field == null && prop == null)
            {
                return false;
            }

            return true;
        }
    }
}
using System.Collections.Generic;
using Newtonsoft.Json;
using Talent.Models.JsonSettings;

namespace Talent.Models.Boolean
{
   
    public class BooleanPhraseModel : BooleanModel
    {
        public BooleanOperator OperatorType { get; set; }
       
        [JsonConverter(typeof(BooleanModelConverter))]
        public List<BooleanModel> Children { get; set; }

        public BooleanPhraseModel()
        {
            Children = new ();
        }

        public override bool CheckCondition<T>(T o)
        {
            bool andResult = true;
            bool orResult = false;
            foreach (var child in Children)
            {
                bool childResult = child.CheckCondition<T>(o);
                andResult = andResult && childResult;
                orResult = orResult || childResult;
            }

            return CalculateResult(andResult, orResult);
        }

        private bool CalculateResult(bool andResult, bool orResult)
        {
            bool isOperatorOr = OperatorType == BooleanOperator.OR;
            return (isOperatorOr && orResult) || (!isOperatorOr && andResult);
        }
    }
    
    
}
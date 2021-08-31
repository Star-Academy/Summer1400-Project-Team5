using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Talent.Models.JsonSettings;

namespace Talent.Models
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

        public bool CheckCondition<T>(T o)
        {
            throw new NotImplementedException();
        }
    }
    
    
}
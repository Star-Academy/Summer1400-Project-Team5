using System;
using System.Collections.Generic;

namespace Talent.Models
{
    public class BooleanPhraseModel : BooleanModel
    {
        public List<BooleanModel> Children { get; set; }

        public BooleanPhraseModel()
        {
            Children = new List<BooleanModel>();
        }
    }
    
    
}
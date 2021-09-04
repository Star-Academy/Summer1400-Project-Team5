using Talent.Models.Boolean;

namespace Talent.Models
{
    public class FilterModel : ProcessModel
    {
        public BooleanPhraseModel Filter { get; set; }
    }
}
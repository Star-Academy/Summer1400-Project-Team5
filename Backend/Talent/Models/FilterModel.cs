namespace Talent.Models
{
    public class FilterModel : ProcessModel
    {
        public object FilterObject { get; set; }
        public string ToSql()
        {
            return "A=B AND (C>D OR E<F)";
        }
    }
}
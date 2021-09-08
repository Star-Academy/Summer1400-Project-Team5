namespace Talent.Models
{
    public enum JoinMethod
    {
        INNER, OUTER, LEFT, RIGHT
    }
    public class JoinModel : ProcessModel
    {
        public JoinMethod JoinMethod { get; set; }
        public string SourceKey { get; set; }
        public string AddSourceKey { get; set; }

        public int AddSourceId { get; set; }
    }
}
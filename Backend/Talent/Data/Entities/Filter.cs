using System.ComponentModel.DataAnnotations;

namespace Talent.Data.Entities
{
    public class Filter : Process
    {
        [Key] public int Id { get; set; }
    }
}
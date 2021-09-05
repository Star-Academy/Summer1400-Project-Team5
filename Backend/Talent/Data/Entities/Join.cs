using System.ComponentModel.DataAnnotations;

namespace Talent.Data.Entities
{
    public class Join : Process
    {
        [Key] public int Id { get; set; }
    }
}
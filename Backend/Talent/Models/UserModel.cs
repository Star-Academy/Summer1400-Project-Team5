using System.ComponentModel.DataAnnotations;
namespace Talent.Models
{
    public class UserModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
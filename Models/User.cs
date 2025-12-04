using System.ComponentModel.DataAnnotations;

namespace Healthcare.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;

        [Required]
        public string Role { get; set; } = "User";

        public bool IsActive { get; set; } = true;
    }
}

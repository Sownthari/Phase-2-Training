using System.ComponentModel.DataAnnotations;

namespace SocialMediaApplication.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public ICollection<Post>? posts { get; set; }
    }
}

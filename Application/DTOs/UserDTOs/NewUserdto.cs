using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.UserDTOs
{
    public class NewUserdto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }
}

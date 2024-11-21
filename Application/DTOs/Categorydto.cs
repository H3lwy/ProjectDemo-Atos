using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class Categorydto
    {
        [MaxLength(255)]
        [Required]
        public string CategoryName { get; set; }
    }
}

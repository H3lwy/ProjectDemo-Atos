using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectDemo.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        [MaxLength(255)]
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public double Price { get; set; }
        public int stock { get; set; }
    }
}

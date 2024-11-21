using Microsoft.AspNetCore.Identity;

namespace ProjectDemo.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;

namespace CinemaAdminApp.Models
{
    public class CinemaAppUsers : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

      
    }
}

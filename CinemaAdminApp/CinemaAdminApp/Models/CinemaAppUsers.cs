using Microsoft.AspNetCore.Identity;

namespace EShopAdminApplication.Models
{
    public class EShopApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

      
    }
}

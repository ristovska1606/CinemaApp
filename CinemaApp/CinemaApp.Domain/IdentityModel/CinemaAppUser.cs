using CinemaApp.Domain.DomainModels;
using Microsoft.AspNetCore.Identity;

namespace CinemaApp.Domain.IdentityModel
{
    public class CinemaAppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public virtual ShoppingCart ShoppingCart { get; set; }
    }
}

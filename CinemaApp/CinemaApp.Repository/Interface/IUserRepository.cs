using CinemaApp.Domain.IdentityModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaApp.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<CinemaAppUser> GetAll();
        CinemaAppUser Get(string id);
        
    }
}

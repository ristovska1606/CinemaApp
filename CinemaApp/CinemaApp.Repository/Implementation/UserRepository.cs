using CinemaApp.Domain.IdentityModel;
using CinemaApp.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaApp.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private DbSet<CinemaAppUser> entities;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
            entities = _context.Set<CinemaAppUser>();
        }
        public CinemaAppUser Get(string id)
        {
            return entities
                .Include(z => z.ShoppingCart)
                .Include("ShoppingCart.TicketInShoppingCarts")
                .Include("ShoppingCart.TicketInShoppingCarts.Ticket")
                .SingleOrDefault(z => z.Id == id);
                
        }

        public IEnumerable<CinemaAppUser> GetAll()
        {
            return entities.Include(z => z.ShoppingCart)
                .Include("ShoppingCart.TicketInShoppingCarts")
                .Include("ShoppingCart.TicketInShoppingCarts.Ticket")
                .AsEnumerable();
        }
    }
}

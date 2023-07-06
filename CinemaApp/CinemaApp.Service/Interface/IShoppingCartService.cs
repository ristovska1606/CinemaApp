using CinemaApp.Domain.DomainModels;
using CinemaApp.Domain.DTO;
using CinemaApp.Domain.Relationships;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema.Service.Interface
{
    public interface IShoppingCartService
    {
        public ShoppingCartDto GetTicketForShoppingCart(string userId);

        public TicketInShoppingCart DeleteTicketFromShoppingCart(Guid? ticketId, string userId);

        public Order CreateOrder(string userId);
    }
}

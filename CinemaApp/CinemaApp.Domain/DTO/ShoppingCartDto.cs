
using CinemaApp.Domain.Relationships;
using System.Collections.Generic;

namespace CinemaApp.Domain.DTO
{
    public class ShoppingCartDto
    {
        public List<TicketInShoppingCart> Ticket { get; set; }
        public double TotalPrice { get; set; }
    }
}

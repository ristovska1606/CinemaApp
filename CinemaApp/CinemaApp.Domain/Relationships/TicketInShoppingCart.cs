using CinemaApp.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApp.Domain.Relationships
{
    public class TicketInShoppingCart : BaseEntity
    {
        public Guid TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }

        public Guid ShoppingCartId { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
    }
}

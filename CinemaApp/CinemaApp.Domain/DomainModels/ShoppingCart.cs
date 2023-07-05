using CinemaApp.Domain.IdentityModel;
using CinemaApp.Domain.Relationships;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaApp.Domain.DomainModels
{
    public class ShoppingCart : BaseEntity
    {
        public string OwnerId { get; set; }
        public virtual CinemaAppUser Owner { get; set; }
        public virtual ICollection<TicketInShoppingCart> TicketsInShoppingCart { get; set; }
    }
}

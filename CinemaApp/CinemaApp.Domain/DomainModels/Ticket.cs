﻿using CinemaApp.Domain.Relationships;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaApp.Domain.DomainModels
{
    public class Ticket : BaseEntity
    {
        public int TicketPrice { get; set; }
        public int SeatNumber { get; set; }
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }
        public virtual ICollection<TicketInShoppingCart> TicketInShoppingCarts { get; set; }
        public virtual ICollection<TicketInOrder> TicketInOrders { get; set; }
    }
}

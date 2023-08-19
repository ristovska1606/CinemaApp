using CinemaAdminApp.Models;
using System;
using System.Collections.Generic;

namespace CinemaAdminApp.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string OwnerId { get; set; }
        public virtual CinemaAppUsers Owner { get; set; }

        public virtual ICollection<TicketInOrder> TicketInOrders { get; set; }
    }
}

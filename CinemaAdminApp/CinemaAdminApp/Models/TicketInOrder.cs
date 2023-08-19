using CinemaAdminApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaAdminApp.Models
{
    public class TicketInOrder 
    {
        public Guid ProductId { get; set; }
        public virtual Ticket Product { get; set; }

        public Guid OrderId { get; set; }
        public virtual Order UserOrder { get; set; }
        public int Quantity { get; set; }
    }
}

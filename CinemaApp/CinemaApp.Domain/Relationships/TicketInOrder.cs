using CinemaApp.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApp.Domain.Relationships
{
    public class TicketInOrder : BaseEntity
    {
        public Guid TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }

        public Guid OrderId { get; set; }
        public virtual Order UserOrder { get; set; }
        
    }
}

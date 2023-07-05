
using CinemaApp.Domain.IdentityModel;
using CinemaApp.Domain.Relationships;
using System;
using System.Collections.Generic;

namespace CinemaApp.Domain.DomainModels
{
    public class Order : BaseEntity
    {
        public string OwnerId { get; set; }
        public virtual CinemaAppUser Owner { get; set; }

        public virtual ICollection<TicketInOrder> TicketsInOrder { get; set; }
    }
}

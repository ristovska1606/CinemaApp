using CinemaApp.Domain.DomainModels;
using System;

namespace CinemaApp.Domain.DTO
{
    public class AddToShoppingCardDto
    {
        public Ticket SelectedTicket { get; set; }
        public Guid SelectedTicketId { get; set; }
        
    }
}

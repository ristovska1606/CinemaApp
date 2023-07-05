using CinemaApp.Domain.DomainModels;
using CinemaApp.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaApp.Service.Interface
{
    public interface ITicketService
    {
        public List<Ticket> GetAllTicketsAsList();
        public Ticket GetSpecificTicket(Guid? id);
        public Ticket CreateNewTicket(Ticket newEntity);
        public Ticket UpdateExistingTicket(Ticket updatedTicket);
        public Ticket DeleteTicket(Guid? id);
        public Boolean TicketExist(Guid? id);
        public void ChangeTicketStatus(Guid? id);

        public AddToShoppingCardDto GetAddToShoppingCartDto(Guid? id);

        public Boolean AddTicketInShoppingCart(AddToShoppingCardDto entity, string userId);
    }
}

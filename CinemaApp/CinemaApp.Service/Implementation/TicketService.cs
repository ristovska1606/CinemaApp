using CinemaApp.Domain.DomainModels;
using CinemaApp.Domain.DTO;
using CinemaApp.Domain.Enums;
using CinemaApp.Domain.Relationships;
using CinemaApp.Repository.Interface;
using CinemaApp.Service.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaApp.Service.Implementation
{
    public class TicketService : ITicketService
    {
        private readonly IRepository<Ticket> _ticketRepository;
        private readonly IRepository<TicketInShoppingCart> _ticketInShoppingCartRepository;
        private readonly IUserRepository _userRepository;
        

        public TicketService(IRepository<Ticket> ticketRepository, IRepository<TicketInShoppingCart> ticketInShoppingCartRepository, IUserRepository userRepository)
        {
            _ticketRepository = ticketRepository;
            _ticketInShoppingCartRepository = ticketInShoppingCartRepository;
            _userRepository = userRepository;
           
        }

        public Boolean AddTicketInShoppingCart(AddToShoppingCardDto entity, string userId)
        {
            var ticket = this.GetSpecificTicket(entity.SelectedTicketId);

            var loggedInUser = _userRepository.Get(userId);

            var UserCart = loggedInUser.ShoppingCart;

            if (ticket != null && loggedInUser != null && UserCart != null)
            {
                var itemToAdd = new TicketInShoppingCart
                {
                    Id = Guid.NewGuid(),
                    ShoppingCart = UserCart,
                    Ticket = ticket,
                    TicketId = ticket.Id,
                    ShoppingCartId = UserCart.Id
                };

                _ticketInShoppingCartRepository.Insert(itemToAdd);

                
                return true;
            }else
            {
              
                return false;
            }
            
        }

        public void ChangeTicketStatus(Guid? id)
        {
            _ticketRepository.Get(id).TicketStatus = TicketStatus.UNAVAILABLE;
        }

        public Ticket CreateNewTicket(Ticket newEntity)
        {
            newEntity.Id = Guid.NewGuid();
            return _ticketRepository.Insert(newEntity);
        }

        public Ticket DeleteTicket(Guid? id)
        {
            var ticketToDelete = this.GetSpecificTicket(id);
            return _ticketRepository.Delete(ticketToDelete);
        }

        public AddToShoppingCardDto GetAddToShoppingCartDto(Guid? id)
        {
            var selectedTicket = this.GetSpecificTicket(id);


            var model = new AddToShoppingCardDto
            {
                SelectedTicket = selectedTicket,
                SelectedTicketId = selectedTicket.Id
            };

            return model;
        }

        public List<Ticket> GetAllTicketsAsList()
        {
            return _ticketRepository.GetAll().ToList();
        }

        public Ticket GetSpecificTicket(Guid? id)
        {
            return _ticketRepository.Get(id);
        }

        public bool TicketExist(Guid? id)
        {
            var ticketExist = this.GetSpecificTicket(id);

            return ticketExist != null;
        }

        public Ticket UpdateExistingTicket(Ticket updatedTicket)
        {
            return _ticketRepository.Update(updatedTicket);
        }
    }
}

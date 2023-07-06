using Cinema.Service.Interface;
using CinemaApp.Domain.DomainModels;
using CinemaApp.Domain.DTO;
using CinemaApp.Domain.Relationships;
using CinemaApp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaApp.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepository<TicketInShoppingCart> _ticketInShoppingCartRepository;
        private readonly IRepository<TicketInOrder> _ticketInOrderRepository;
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<Order> _orderRepository;
       

        public ShoppingCartService(IUserRepository userRepository, IRepository<TicketInShoppingCart> ticketInShoppingCartRepositor, IRepository<TicketInOrder> ticketInOrderRepository, IRepository<ShoppingCart> shoppingCartRepository, IRepository<Order> orderRepository)
        {
            _userRepository = userRepository;
            _ticketInShoppingCartRepository = ticketInShoppingCartRepositor;
            _ticketInOrderRepository = ticketInOrderRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _orderRepository = orderRepository;
        }

        public Order CreateOrder(string userId)
        {
            var loggedInUser = _userRepository.Get(userId);

            var UserCart = loggedInUser.ShoppingCart;

            //EmailMessage emailMessage = new EmailMessage();
            //emailMessage.MailTo = loggedInUser.Email;
            //emailMessage.Subject = "Successfully created order.";
            //emailMessage.Status = false;
            //emailMessage.Content = "Successfully created order."; //to be edited

            Order userOrder = new Order
            {
                Id = Guid.NewGuid(),
                OwnerId = loggedInUser.Id,
                Owner = loggedInUser
            };

            _orderRepository.Insert(userOrder);

            var productsInOrder = UserCart.TicketsInShoppingCart.Select(z => new TicketInOrder
            {
                Id = Guid.NewGuid(),
                TicketId = z.Ticket.Id,
                Ticket = z.Ticket,
                OrderId = userOrder.Id,
                UserOrder = userOrder
            }).ToList();

            foreach (var item in productsInOrder)
            {
                _ticketInOrderRepository.Insert(item);
            }

            UserCart.TicketsInShoppingCart.Clear();

            _shoppingCartRepository.Update(UserCart);

            return userOrder;

        }

        public TicketInShoppingCart DeleteTicketFromShoppingCart(Guid? productId, string userId)
        {
            var loggedInUser = _userRepository.Get(userId);

            var UserCart = loggedInUser.ShoppingCart;

            var itemToDelete = _ticketInShoppingCartRepository.Get(productId);

            UserCart.TicketsInShoppingCart.Remove(itemToDelete);

            _shoppingCartRepository.Update(UserCart);

            return itemToDelete;
        }

        public ShoppingCartDto GetTicketForShoppingCart(string userId)
        {
            var loggedInUser = _userRepository.Get(userId);

            var UserCart = loggedInUser.ShoppingCart;

            var allTickets = UserCart.TicketsInShoppingCart.ToList();

            var allTicketPrices = allTickets.Select(z => new
            {
                TicketPrice = z.Ticket.TicketPrice,
                
            }).ToList();

            double totalPrice = 0.0;

            foreach (var item in allTicketPrices)
            {
                totalPrice +=  item.TicketPrice;
            }

            ShoppingCartDto model = new ShoppingCartDto
            {
                Ticket = allTickets,
                TotalPrice = totalPrice
            };

            return model;
            
        }
    }
}

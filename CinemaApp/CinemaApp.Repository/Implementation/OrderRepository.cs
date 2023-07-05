using CinemaApp.Domain;
using CinemaApp.Domain.DomainModels;
using CinemaApp.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaApp.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private DbSet<Order> entities;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
            entities = _context.Set<Order>();
        }
        public List<Order> GetAllOrders()
        {
            return entities
                .Include(z => z.TicketsInOrder)
                .Include(z => z.Owner)
                .Include("TicketInOrder.Ticket")
                .ToListAsync().Result;
        }

        public Order GetOrderDetails(BaseEntity model)
        {
            return entities
                 .Include(z => z.TicketsInOrder)
                 .Include(z => z.Owner)
                 .Include("TicketsInOrder.Ticket")
                 .SingleOrDefaultAsync(z => z.Id == model.Id).Result;
        }
    }
}

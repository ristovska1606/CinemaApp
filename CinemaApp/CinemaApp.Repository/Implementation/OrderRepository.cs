using EShop.Domain;
using EShop.Domain.DomainModels;
using EShop.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EShop.Repository.Implementation
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
                .Include(z => z.ProductInOrders)
                .Include(z => z.Owner)
                .Include("ProductInOrders.Product")
                .ToListAsync().Result;
        }

        public Order GetOrderDetails(BaseEntity model)
        {
            return entities
                 .Include(z => z.ProductInOrders)
                 .Include(z => z.Owner)
                 .Include("ProductInOrders.Product")
                 .SingleOrDefaultAsync(z => z.Id == model.Id).Result;
        }
    }
}

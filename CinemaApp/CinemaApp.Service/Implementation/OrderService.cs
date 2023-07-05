﻿using EShop.Domain;
using EShop.Domain.DomainModels;
using EShop.Domain.Relationship;
using EShop.Repository.Interface;
using EShop.Service.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;   
        }
        public List<Order> GetAllOrders()
        {
            return this._orderRepository.GetAllOrders();
        }

        public Order GetOrderDeatils(BaseEntity model)
        {
            return this._orderRepository.GetOrderDetails(model);
        }
    }
}

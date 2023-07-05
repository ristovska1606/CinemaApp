using CinemaApp.Domain;
using CinemaApp.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaApp.Service.Interface
{
    public interface IOrderService
    {
        List<Order> GetAllOrders();
        Order GetOrderDeatils(BaseEntity model);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sublihome.Application.Dto.Orders;

namespace Sublihome.Application.Orders
{
    public interface IOrderService
    {
        Task Create(NewOrderDto newOrderDto);

        Task<List<OrdersDto>> GetOrders(int userId);

        Task<OrdersDto> GetOrder(int orderId);

        Task ChangeOrderStatus(int orderId, int orderStatus);

        Task<List<OrderDto>> GetAllOrders();
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sublihome.Application.Dto.Orders;
using Sublihome.Application.Orders;

namespace Sublihome.Web.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Route("GetAllOrdersWithItems")]
        public async Task<List<OrdersDto>> GetOrders(int userId)
        {
            return await _orderService.GetOrders(userId);
        }

        [HttpGet]
        [Route("GetOrder")]
        public async Task<OrdersDto> GetOrder(int orderId)
        {
            return await _orderService.GetOrder(orderId);
        }

        [HttpPut]
        [Route("ChangeOrderStatus")]
        public async Task GetOrders(int orderId, int orderStatus)
        {
            await _orderService.ChangeOrderStatus(orderId, orderStatus);
        }

        [HttpPost]
        [Route("CreateNewOrder")]
        public async Task CreateOrder(NewOrderDto newOrderDto)
        {
            await _orderService.Create(newOrderDto);
        }

        [HttpGet]
        [Route("GetAllOrders")]
        public async Task<List<OrderDto>> GetAllOrders()
        {
            return await _orderService.GetAllOrders();
        }
    }
}

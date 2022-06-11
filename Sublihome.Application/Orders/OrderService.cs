using Sublihome.Application.Dto.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sublihome.Application.Carts;
using Sublihome.Application.Helper;
using Sublihome.Data.Entities.Orders;
using Sublihome.Data.Entities.Products;
using Sublihome.Data.GenericRepository;

namespace Sublihome.Application.Orders
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderProducts> _orderProductsRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<OrderStatus> _orderStatusRepository;
        private readonly ICartService _cartService;

        public OrderService(
            ILogger<OrderService> logger,
            IMapper mapper,
            IRepository<Order> orderRepository,
            IRepository<OrderProducts> productsRepository,
            ICartService cartService,
            IRepository<Product> productRepository,
            IRepository<OrderStatus> orderStatusRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _orderRepository = orderRepository;
            _orderProductsRepository = productsRepository;
            _cartService = cartService;
            _productRepository = productRepository;
            _orderStatusRepository = orderStatusRepository;
        }
        public async Task ChangeOrderStatus(int orderId, int orderStatus)
        {
            var order = await _orderRepository.GetAsync(orderId);

            if (order == null)
            {
                _logger.LogError($"Unable to find order with Id: {orderId}");
                throw new UserFriendlyException("Unable to find such order");
            }

            order.StatusId = orderStatus;

            _orderRepository.Update(order);
        }

        public async Task Create(NewOrderDto newOrderDto)
        {
            var count = 0;
            decimal totalPrice = 0;

            var newOrder = new Order
            {
                UserId = newOrderDto.UserId,
                StatusId = (int) OrderStatusEnum.Pending
            };

            await _orderRepository.AddAsync(newOrder);

            var newOrders = await _orderRepository.GetAll()
                .Where(x => x.UserId == newOrderDto.UserId)
                .ToListAsync();

            var cartItems = await _cartService.GetAllProductFromCart(newOrderDto.UserId);

            foreach (var orderedItem in cartItems)
            {
                var newOrderItem = new OrderProducts
                {
                    ProductId = orderedItem.ProductId,
                    Count = orderedItem.Count,
                    OrderId = newOrders.LastOrDefault().Id
                };

                var orderedItemPrice = await _productRepository.GetAll()
                    .Where(x => x.Id == orderedItem.ProductId)
                    .FirstOrDefaultAsync();

                totalPrice += (orderedItemPrice.Price * orderedItem.Count);

                await _orderProductsRepository.AddAsync(newOrderItem);
            }

            newOrder.TotalPrice = totalPrice;

            var order = await _orderRepository.GetAll()
                .ToListAsync();

            var updateOrder = order.LastOrDefault();

            updateOrder.TotalPrice = totalPrice;

            _orderRepository.Update(updateOrder);

            await _cartService.ClearCartItems(newOrderDto.UserId);
        }

        public async Task<List<OrdersDto>> GetOrders(int userId)
        {
            var allOrdersWithItems = new List<OrdersDto>();
            var count = 0;

            var orders = await _orderRepository.GetAll()
                .Where(x => x.UserId == userId)
                .Select(x => x.Id)
                .ToListAsync();

            var orderStatuses = await _orderRepository.GetAll()
                .Where(x => x.UserId == userId)
                .Select(x => x.StatusId)
                .ToListAsync();

            foreach (var orderId in orders)
            {
                decimal totalPriceOfOrder = 0;

                var allOrderItemsIds = await _orderProductsRepository.GetAll()
                    .Where(x => x.OrderId.Equals(orderId))
                    .Select(x => x.ProductId)
                    .ToListAsync();

                var allOrderItemsCounts = await _orderProductsRepository.GetAll()
                    .Where(x => x.OrderId.Equals(orderId))
                    .Select(x => x.Count)
                    .ToListAsync();

                var orderPrice = await _orderRepository.GetAll()
                    .Where(x => x.Id == orderId)
                    .Select(x => x.TotalPrice)
                    .FirstOrDefaultAsync();

                var orderWitItems = new OrdersDto
                {
                    Order = orderId,
                    ProductIds = allOrderItemsIds,
                    ProductsCount = allOrderItemsCounts,
                    TotalPriceOfOrder = orderPrice,
                    StatusId = orderStatuses[count++]
                };

                allOrdersWithItems.Add(orderWitItems);
            }

            return allOrdersWithItems;
        }

        public async Task<OrdersDto> GetOrder(int orderId)
        {
            var orders = await _orderRepository.GetAll()
                .Where(x => x.Id == orderId)
                .ToListAsync();

            var allOrderItemsIds = await _orderProductsRepository.GetAll()
                   .Where(x => x.OrderId.Equals(orderId))
                   .Select(x => x.ProductId)
                   .ToListAsync();

            var allOrderItemsCounts = await _orderProductsRepository.GetAll()
                .Where(x => x.OrderId.Equals(orderId))
                .Select(x => x.Count)
                .ToListAsync();

            var orderPrice = await _orderRepository.GetAll()
                .Where(x => x.Id == orderId)
                .Select(x => x.TotalPrice)
                .FirstOrDefaultAsync();

            var orderWitItems = new OrdersDto
            {
                Order = orderId,
                ProductIds = allOrderItemsIds,
                ProductsCount = allOrderItemsCounts,
                TotalPriceOfOrder = orderPrice
            };

            return orderWitItems;
        }

        public async Task<List<OrderDto>> GetAllOrders()
        {
            var allOrders = await _orderRepository.GetAll()
                .ToListAsync();

            return _mapper.Map<List<OrderDto>>(allOrders);
        }
    }
}

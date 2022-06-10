using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sublihome.Application.Carts;
using Sublihome.Application.Dto.Carts;

namespace Sublihome.Web.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost]
        [Route("UpdateCart")]
        public async Task UpdateCart(NewCartProductsDto newCartProductsDto)
        {
            await _cartService.UpdateUserCart(newCartProductsDto);
        }

        [HttpGet]
        [Route("GetItemsFromCart")]
        public async Task<List<CartProductsDto>> GetCart(int userId)
        {
            return await _cartService.GetAllProductFromCart(userId);
        }

        [HttpDelete]
        [Route("ClearCart")]
        public async Task ClearCartItems(int userId)
        {
            await _cartService.ClearCartItems(userId);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sublihome.Application.Dto.Carts;

namespace Sublihome.Application.Carts
{
    public interface ICartService
    {
        Task Create();

        Task Delete(int userId);

        Task UpdateUserCart(NewCartProductsDto newCartProductsDto);

        Task<List<CartProductsDto>> GetAllProductFromCart(int userId);

        Task ClearCartItems(int userId);
    }
}

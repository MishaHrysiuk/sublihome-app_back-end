using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sublihome.Application.Dto.Carts;
using Sublihome.Data.Entities.Carts;
using Sublihome.Data.Entities.Products;
using Sublihome.Data.Entities.Users;
using Sublihome.Data.GenericRepository;

namespace Sublihome.Application.Carts
{
    public class CartService : ICartService
    {
        private readonly ILogger<CartService> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<CartProducts> _cartProductsRepository;
        private readonly IRepository<Product> _productRepository;

        public CartService(
            ILogger<CartService> logger,
            IMapper mapper,
            IRepository<Cart> cartRepository,
            IRepository<User> userRepository,
            IRepository<CartProducts> cartProductsRepository,
            IRepository<Product> productRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _cartRepository = cartRepository;
            _userRepository = userRepository;
            _cartProductsRepository = cartProductsRepository;
            _productRepository = productRepository;
        }

        public async Task Create()
        {
            var lastCreatedUser = await _userRepository.GetAll()
                .ToListAsync();

            var newUserCart = new Cart
            {
                UserId = lastCreatedUser.LastOrDefault().Id
            };

            await _cartRepository.AddAsync(newUserCart);
        }

        public async Task UpdateUserCart(NewCartProductsDto newCartProductsDto)
        {
            int count = 0;

            var userCart = await _cartRepository.GetAll()
                .FirstOrDefaultAsync(x => x.UserId == newCartProductsDto.UserId);

            var existingProductsInCart = await _cartProductsRepository.GetAll()
                .Where(x => x.CartId == userCart.Id)
                .ToListAsync();

            if (existingProductsInCart.Count == 0)
            {
                foreach (var productId in newCartProductsDto.ProductsList)
                {
                    var productCount = newCartProductsDto.ProductsCount[count++];

                    var newCartProduct = new CartProducts
                    {
                        CartId = userCart.Id,
                        ProductId = productId,
                        Count = productCount
                    };

                    await _cartProductsRepository.AddAsync(newCartProduct);
                }
            }
            else
            {
                foreach (var existingProduct in existingProductsInCart)
                {
                    _cartProductsRepository.Delete(existingProduct);
                }

                foreach (var productId in newCartProductsDto.ProductsList)
                {
                    var productCount = newCartProductsDto.ProductsCount[count++];

                    var newCartProduct = new CartProducts
                    {
                        CartId = userCart.Id,
                        ProductId = productId,
                        Count = productCount
                    };

                    await _cartProductsRepository.AddAsync(newCartProduct);
                }
            }
        }

        public async Task<List<CartProductsDto>> GetAllProductFromCart(int userId)
        {
            var userCart = await _cartRepository.GetAll()
                .FirstOrDefaultAsync(x => x.UserId == userId);

            var existingProductsInCart = await _cartProductsRepository.GetAll()
                .Where(x => x.CartId == userCart.Id)
                .Include(x => x.Product)
                .ToListAsync();

            //var listOfProducts = new CartProductsDto
            //{
            //    ProductsCount = new List<int>(),
            //    ProductsList = new List<int>()
            //};

            var test = new List<CartProductsDto>();

            foreach (var existingProduct in existingProductsInCart)
            {
                test.Add(new CartProductsDto
                {
                    Id = existingProduct.Id,
                    ProductId = existingProduct.ProductId,
                    Count = existingProduct.Count,
                    Name = existingProduct.Product.Name,
                    Price = existingProduct.Product.Price,
                    Picture = RetrievePicture(existingProduct.Product.Picture, existingProduct.Product.Name)
                });
            }

            return test;
        }

        private FileContentResult RetrievePicture(byte[] picture, string productName)
        {
            string mimeType = "application/png";

            return new FileContentResult(picture, mimeType)
            {
                FileDownloadName = $"{productName}.png"
            };
        }

        public async Task Delete(int userId)
        {
            var cart = await _cartRepository.GetAll()
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync();

            _cartRepository.Delete(cart);
        }

        public async Task ClearCartItems(int userId)
        {
            var cart = await _cartRepository.GetAll()
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync();

            var existingProductsInCart = await _cartProductsRepository.GetAll()
                .Where(x => x.CartId == cart.Id)
                .ToListAsync();

            foreach (var existingProduct in existingProductsInCart)
            {
                _cartProductsRepository.Delete(existingProduct);
            }
        }
    }
}

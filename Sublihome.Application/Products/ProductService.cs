using Sublihome.Application.Dto.Products;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sublihome.Application.Helper;
using Sublihome.Data.Entities.Products;
using Sublihome.Data.GenericRepository;

namespace Sublihome.Application.Products
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductType> _productTypeRepository;

        public ProductService(
            IMapper mapper,
            ILogger<ProductService> logger,
            IRepository<Product> productRepository,
            IRepository<ProductType> productTypeRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _logger = logger;
            _productTypeRepository = productTypeRepository;
        }

        public async Task Create(NewProductDto newProductDto)
        {
            var existingProductType = await _productTypeRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Name.ToLower().Equals(newProductDto.ProductType.ToLower()));

            if (existingProductType == null)
            {
                _logger.LogError("Unable to create new product due to undefined ProductType");
                throw new UserFriendlyException("Unable to create new product. Check spelling of Product Type name");
            }

            var newProduct = new Product
            {
                Name = newProductDto.Name,
                ProductTypeId = existingProductType.Id,
                Price = newProductDto.Price
            };

            await _productRepository.AddAsync(newProduct);
        }

        public async Task<ProductDto> GetById(int productId)
        {
            var product = await _productRepository.GetAll()
                .Include(x => x.ProductType)
                .FirstOrDefaultAsync(x => x.Id == productId);

            if (product == null)
            {
                _logger.LogError($"Unable to find product with id {productId}");
                throw new UserFriendlyException("Unable to find product");
            }

            var returnedProduct = _mapper.Map<ProductDto>(product);

            if (product.Picture != null)
            {
                var picture = RetrievePicture(product.Picture, product.Name);
                returnedProduct.ProductPicture = picture;
            }

            return returnedProduct;
        }

        public async Task<List<ProductDto>> GetAll()
        {
            var products = await _productRepository.GetAll()
                .ToListAsync();

            var productsDto = _mapper.Map<List<ProductDto>>(products);

            foreach (var product in products)
            {
                if (product.Picture != null)
                {
                    var picture = RetrievePicture(product.Picture, product.Name);
                    productsDto.FirstOrDefault(x => x.Id == product.Id).ProductPicture = picture;
                }
            }

            return productsDto;
        }

        public async Task Update(UpdatedProductDto updatedProductDto)
        {
            var product = await _productRepository.GetAsync(updatedProductDto.Id);

            if (product == null)
            {
                _logger.LogError($"Unable to find product with id {updatedProductDto.Id} and updated it");
                throw new UserFriendlyException("Unable to update product");
            }

            product.Name = updatedProductDto.Name;
            product.Price = updatedProductDto.Price;

            _productRepository.Update(product);
        }

        public async Task Delete(int productId)
        {
            var product = await _productRepository.GetAsync(productId);

            if (product == null)
            {
                _logger.LogError($"Unable to find product with id {productId} and delete it");
                throw new UserFriendlyException("Such product don't exist");
            }

            _productRepository.Delete(product);
        }

        public async Task AddPicture(int productId, IFormFile picture)
        {
            var product = await _productRepository.GetAsync(productId);

            if (product == null)
            {
                _logger.LogError($"Unable to find product with id {productId} and add picture to it");
                throw new UserFriendlyException("Such product don't exist");
            }

            if (picture.Length > 0)
            {
                using (var toTarget = new MemoryStream())
                {
                    await picture.CopyToAsync(toTarget);
                    product.Picture = toTarget.ToArray();
                }

                _productRepository.Update(product);
            }
            else
            {
                _logger.LogError($"Unable to find product with id {productId} and add picture to it");
                throw new UserFriendlyException("Such product don't exist");
            }
        }

        public async Task<FileContentResult> DownloadPicture(int productId)
        {
            var product = await _productRepository.GetAsync(productId);

            if (product == null)
            {
                _logger.LogError($"Unable to find product with id {productId} and retrieve picture from it");
                throw new UserFriendlyException("Such product don't exist");
            }

            if (product.Picture == null)
            {
                _logger.LogError($"Unable to retrieve picture from this item because there is none");
                throw new UserFriendlyException("No picture for that item");
            }

            byte[] picture = product.Picture;
            string mimeType = "image/png";

            return new FileContentResult(picture, mimeType)
            {
                FileDownloadName = $"{product.Name}.png"
            };
        }

        private FileContentResult RetrievePicture(byte[] picture, string productName)
        {
            string mimeType = "application/png";

            return new FileContentResult(picture, mimeType)
            {
                FileDownloadName = $"{productName}.png"
            };
        }
    }
}

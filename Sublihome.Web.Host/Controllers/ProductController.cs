using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sublihome.Application.Dto.Products;
using Sublihome.Application.Products;

namespace Sublihome.Web.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [Route("CreateProduct")]
        public async Task CreateNewProduct(NewProductDto newProductDto)
        {
            await _productService.Create(newProductDto);
        }

        [HttpPost]
        [Route("AddPictureToProduct")]
        public async Task AddPicture(int productId, IFormFile file)
        {
            await _productService.AddPicture(productId, file);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetProduct")]
        public async Task<ProductDto> GetProductById(int productId)
        {
            return await _productService.GetById(productId);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetAllProducts")]
        public async Task<List<ProductDto>> GetAllProducts()
        {
            return await _productService.GetAll();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("DownloadPictureFromProduct")]
        public async Task<FileContentResult> DownloadPicture(int productId)
        {
            return await _productService.DownloadPicture(productId);
        }

        [HttpPut]
        [Route("UpdateProduct")]
        public async Task UpdateProduct(UpdatedProductDto updatedProductDto)
        {
            await _productService.Update(updatedProductDto);
        }

        [HttpDelete]
        [Route("DeleteProduct")]
        public async Task DeleteProduct(int productId)
        {
            await _productService.Delete(productId);
        }
    }
}

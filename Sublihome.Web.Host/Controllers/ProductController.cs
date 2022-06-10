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
        //[Authorize(Roles = "UserIsAdmin")]
        [Route("AddPictureToProduct")]
        public async Task AddPicture(int productId, IFormFile file)
        {
            await _productService.AddPicture(productId, file);
        }

        [HttpGet]
        //[Authorize(Roles = "UserIsAdmin")]
        [Route("GetProduct")]
        public async Task<ProductDto> GetProductById(int productId)
        {
            return await _productService.GetById(productId);
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<List<ProductDto>> GetAllProducts()
        {
            return await _productService.GetAll();
        }

        [HttpGet]
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

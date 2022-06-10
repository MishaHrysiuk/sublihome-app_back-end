using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sublihome.Application.Dto.Products;
using Sublihome.Application.ProductTypes;

namespace Sublihome.Web.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeService _productTypeService;

        public ProductTypeController(IProductTypeService productTypeService)
        {
            _productTypeService = productTypeService;
        }

        [HttpGet]
        [Route("GetProductType")]
        public async Task<ProductTypeDto> GetById(int productTypeId)
        {
            return await _productTypeService.GetById(productTypeId);
        }

        [HttpGet]
        [Route("GetAllProductTypes")]
        public async Task<List<ProductTypeDto>> GetAll()
        {
            return await _productTypeService.GetAll();
        }

        [HttpPost]
        [Route("CreateProductType")]
        public async Task Create(NewProductTypeDto newProductTypeDto)
        {
            await _productTypeService.Create(newProductTypeDto);
        }

        [HttpDelete]
        [Route("DeleteProductType")]
        public async Task Delete(int productTypeId)
        {
            await _productTypeService.Delete(productTypeId);
        }
    }
}

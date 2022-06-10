using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sublihome.Application.Dto.Products;

namespace Sublihome.Application.ProductTypes
{
    public interface IProductTypeService
    {
        Task Create(NewProductTypeDto newProductTypeDto);

        Task<ProductTypeDto> GetById(int productTypeId);

        Task<List<ProductTypeDto>> GetAll();

        Task Delete(int productTypeId);
    }
}

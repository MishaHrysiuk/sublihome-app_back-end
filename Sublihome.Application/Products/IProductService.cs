using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sublihome.Application.Dto.Products;

namespace Sublihome.Application.Products
{
    public interface IProductService
    {
        Task Create(NewProductDto newProductDto);

        Task<ProductDto> GetById(int productId);

        Task<List<ProductDto>> GetAll();

        Task Update(UpdatedProductDto updatedProductDto);

        Task Delete(int productId);

        Task AddPicture(int productId, IFormFile picture);

        Task<FileContentResult> DownloadPicture(int productId);
    }
}

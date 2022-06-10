using Sublihome.Application.Dto.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Sublihome.Application.Helper;
using Sublihome.Data.Entities.Products;
using Sublihome.Data.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace Sublihome.Application.ProductTypes
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly ILogger<ProductTypeService> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<ProductType> _productTypeRepository;

        public ProductTypeService(
            ILogger<ProductTypeService> logger,
            IMapper mapper,
            IRepository<ProductType> productTypeRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _productTypeRepository = productTypeRepository;
        }

        public async Task Create(NewProductTypeDto newProductTypeDto)
        {
            var user = _mapper.Map<ProductType>(newProductTypeDto);

            await _productTypeRepository.AddAsync(user);
        }

        public async Task<List<ProductTypeDto>> GetAll()
        {
            var users = await _productTypeRepository.GetAll()
                .ToListAsync();

            return _mapper.Map<List<ProductTypeDto>>(users);
        }

        public async Task<ProductTypeDto> GetById(int productTypeId)
        {
            var user = await _productTypeRepository.GetAsync(productTypeId);

            if (user == null)
            {
                _logger.LogError($"Unable to find productType with Id: {productTypeId}");
                throw new UserFriendlyException("Unable to find such Product Type");
            }

            return _mapper.Map<ProductTypeDto>(user);
        }

        public async Task Delete(int productTypeId)
        {
            var user = await _productTypeRepository.GetAsync(productTypeId);

            if (user == null)
            {
                _logger.LogError($"Unable to find productType with Id: {productTypeId}");
                throw new UserFriendlyException("Unable to find Product Type");
            }

            _productTypeRepository.Delete(user);
        }
    }
}

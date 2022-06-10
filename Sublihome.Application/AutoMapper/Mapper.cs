using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Sublihome.Application.Dto.Carts;
using Sublihome.Application.Dto.Orders;
using Sublihome.Application.Dto.Products;
using Sublihome.Application.Dto.Users;
using Sublihome.Data.Entities.Orders;
using Sublihome.Data.Entities.Products;
using Sublihome.Data.Entities.Users;

namespace Sublihome.Application.AutoMapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            //Product
            CreateMap<ProductDto, Product>()
                .ReverseMap();

            //User
            CreateMap<UserDto, User>()
                .ReverseMap();
            CreateMap<NewUserDto, User>();

            //ProductType
            CreateMap<ProductTypeDto, ProductType>()
                .ReverseMap();
            CreateMap<NewProductTypeDto, ProductType>();


            CreateMap<CartProductsDto, OrdersDto>()
                //.ForMember(x => x.ProductIds, c => c.MapFrom(x => x.ProductsList))
                .ReverseMap();

            //Orders
            CreateMap<OrderDto, Order>()
                .ReverseMap();
        }
    }
}

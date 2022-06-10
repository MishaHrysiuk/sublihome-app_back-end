using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Sublihome.Application.Authentication;
using Sublihome.Application.Carts;
using Sublihome.Application.Orders;
using Sublihome.Application.Products;
using Sublihome.Application.ProductTypes;
using Sublihome.Application.Users;
using Sublihome.Data.DI;

namespace Sublihome.Application.DI
{
    public static class DependencyInjection
    {
        public static void RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IProductTypeService, ProductTypeService>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.RegisterDataServices();
        }
    }
}

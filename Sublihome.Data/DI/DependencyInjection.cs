using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Sublihome.Data.GenericRepository;

namespace Sublihome.Data.DI
{
    public static class DependencyInjection
    {
        public static void RegisterDataServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}

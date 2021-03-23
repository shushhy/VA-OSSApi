using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using OSS.Data.Repository;

namespace OSS.Services.Services {
    public static class ServiceRegistration {
        public static void AddInfrastructure(this IServiceCollection services) {

            // Customer
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            // Product
            services.AddScoped<IProductRepository, ProductRepository>();
            // Orders
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            // OrderDetails
            services.AddScoped<IOrderDetailsRepository, OrderDetailsRepository>();

            // UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}

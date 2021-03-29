using Microsoft.Extensions.DependencyInjection;
using OSS.Data.Repository;
using OSS.Core.Models;

namespace OSS.Services.Services {
    public static class ServiceRegistration {
        public static void AddInfrastructure(this IServiceCollection services) {

            // Customer
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();
            // Product
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            // Orders
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddScoped<IOrdersService, OrdersService>();
            // OrderDetails
            //services.AddScoped<IOrderDetailsRepository, OrderDetailsRepository>();

            // UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}

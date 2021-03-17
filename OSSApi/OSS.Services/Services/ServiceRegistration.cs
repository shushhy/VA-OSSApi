using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using OSS.Data.Repository;

namespace OSS.Services.Services {
    public static class ServiceRegistration {
        public static void AddInfrastructure(this IServiceCollection services) {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}

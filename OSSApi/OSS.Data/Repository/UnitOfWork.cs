using System;
using System.Collections.Generic;
using System.Text;

namespace OSS.Data.Repository {
    public class UnitOfWork : IUnitOfWork {

        // Customer
        public ICustomerRepository Customers { get; }
        public UnitOfWork(ICustomerRepository customerRepository) {
            Customers = customerRepository;
        }

        // Product
        public IProductRepository Products { get; }
        public UnitOfWork(IProductRepository productRepository)
        {
            Products = productRepository;
        }
    }
}

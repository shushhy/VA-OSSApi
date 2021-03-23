using System;
using System.Collections.Generic;
using System.Text;

namespace OSS.Data.Repository {
    public class UnitOfWork : IUnitOfWork {

        public ICustomerRepository Customers { get; }
        public IProductRepository Products { get; }
        public IOrdersRepository Orders { get; }
        public IOrderDetailsRepository OrderDetails { get; }

        public UnitOfWork(ICustomerRepository customerRepository,
                          IProductRepository productRepository,
                          IOrdersRepository orderRepository,
                          IOrderDetailsRepository orderDetailsRepository)
        {
            Customers = customerRepository;
            Products = productRepository;
            Orders = orderRepository;
            OrderDetails = orderDetailsRepository;
        }
    }
}

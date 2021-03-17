using System;
using System.Collections.Generic;
using System.Text;

namespace OSS.Data.Repository {
    public class UnitOfWork : IUnitOfWork {

        public ICustomerRepository Customers { get; }
        public UnitOfWork(ICustomerRepository customerRepository) {
            Customers = customerRepository;
        }
    }
}

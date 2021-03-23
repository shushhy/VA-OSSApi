using System;
using System.Threading.Tasks;

namespace OSS.Data.Repository {
    public interface IUnitOfWork {
        // Customer
        ICustomerRepository Customers { get; }

        // Product
        IProductRepository Products { get; }

        // Orders
        IOrdersRepository Orders { get; }

        // OrderDetails
        //IOrderDetailsRepository OrderDetails { get; }
    }
}

using System;
using System.Threading.Tasks;

namespace OSS.Data.Repository {
    public interface IUnitOfWork {
        ICustomerRepository Customers { get; }
    }
}

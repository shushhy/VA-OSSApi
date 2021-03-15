using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using OSSApi.Models;

namespace OSSApi.Repository {
    public interface ICustomerRepository {
        Customer GetById(int id);
        List<Customer> GetAll();
        Customer Add(Customer customer);
        Customer Update(Customer customer);
        void Delete(int id);
    }
}

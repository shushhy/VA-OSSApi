using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using OSSApi.Models;
using OSSApi.Repository;

namespace OSSApi.Repository {
    public class CustomerRepository : ICustomerRepository {
        private IDbConnection connection;

        public CustomerRepository(IConfiguration configuration) {
            this.connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }


        public Customer Add(Customer customer) {
            throw new System.NotImplementedException();
        }

        public void Delete(int id) {
            throw new System.NotImplementedException();
        }

        public List<Customer> GetAll() {
            var query = "select * from Customers;";
            return connection.Query<Customer>(query).ToList();
        }

        public Customer GetById(int id) {
            throw new System.NotImplementedException();
        }

        public Customer Update(Customer customer) {
            throw new System.NotImplementedException();
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using OSS.Core.Models;
using OSS.Data.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Dapper.Contrib.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OSS.Services.Services {
    public class CustomerRepository : ICustomerRepository {
        private readonly IConfiguration configuration;

        public CustomerRepository(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task<IReadOnlyList<Customer>> GetAll() {

            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"))) {
                var customers = await connection.GetAllAsync<Customer>();
                return customers.ToList();
            };
        }

        public async Task<Customer> GetById(int id) {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"))) {
                int CustomerId = id;
                var customer = await connection.GetAsync<Customer>(CustomerId);
                return customer;
            }
        }

        public async Task Insert(Customer customer) {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"))) {
                var newCustomer = new Customer {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    Password = customer.Password,
                    Gender = customer.Gender,
                    Country = customer.Country,
                    PhoneNumber = customer.PhoneNumber
                };
                await connection.InsertAsync<Customer>(newCustomer);
            }
        }

        public async Task Update(Customer customer) {
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"))) {
                var updateCustomer = new Customer {
                    CustomerId = customer.CustomerId,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    Password = customer.Password,
                    Gender = customer.Gender,
                    Country = customer.Country,
                    PhoneNumber = customer.PhoneNumber
                };
                await connection.InsertAsync<Customer>(updateCustomer);
            }
        }

        public async Task Delete(int id) {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"))) {
                await connection.DeleteAsync(new Customer { CustomerId = id });
            }
        }
    }
}

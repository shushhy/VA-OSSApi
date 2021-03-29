using System.Collections.Generic;
using System.Threading.Tasks;
using OSS.Core.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Dapper.Contrib.Extensions;

namespace OSS.Data.Repository {
    public class CustomerRepository : ICustomerRepository {
        private readonly IConfiguration configuration;

        public CustomerRepository(IConfiguration configuration) {
            this.configuration = configuration;
        }

        // Select all customer
        public async Task<IReadOnlyList<Customer>> GetAllAsync() {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"))) {
                var customers = await connection.GetAllAsync<Customer>();
                return customers.ToList();
            };
        }

        // Select customer by id
        public async Task<Customer> GetByIdAsync(int id) {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"))) {
                int CustomerId = id;
                var customer = await connection.GetAsync<Customer>(CustomerId);
                return customer;
            }
        }

        // Insert new customer
        public async Task InsertAsync(Customer customer) {
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

        // Edit customer
        public async Task UpdateAsync(Customer customer) {
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
                await connection.UpdateAsync<Customer>(updateCustomer);
            }
        }

        // Delete customer by id
        public async Task DeleteAsync(int id) {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"))) {
                await connection.DeleteAsync(new Customer { CustomerId = id });
            }
        }
    }
}

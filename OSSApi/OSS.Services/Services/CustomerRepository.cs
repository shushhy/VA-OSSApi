using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OSS.Core.Models;
using OSS.Data.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Linq;
using Dapper.Contrib.Extensions;


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

        // Select customer by id
        public async Task<Customer> GetById(int id) {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"))) {
                int CustomerId = id;
                var customer = await connection.GetAsync<Customer>(CustomerId);
                return customer;
            }
        }

        // Insert new customer
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

        // Edit customer
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

        // Delete customer by id
        public async Task<int> Delete(int id) {
            var query = @"DELETE FROM [dbo].[Customer] WHERE CustomerId=@CustomerId;";

            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"))) {
                var affectedRows = await connection.ExecuteAsync(query, new { CustomerId = id });
                return affectedRows;
            }
        }
    }
}

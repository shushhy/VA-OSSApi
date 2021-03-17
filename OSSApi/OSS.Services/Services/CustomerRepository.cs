using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OSS.Core.Models;
using OSS.Data.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;


namespace OSS.Services.Services {
    public class CustomerRepository : ICustomerRepository {
        private readonly IConfiguration configuration;

        public CustomerRepository(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public Task<int> Delete(int id) {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Customer>> GetAll() {
            throw new NotImplementedException();
        }

        public async Task<Customer> GetById(int id) {
            var query = @"SELECT *
                            FROM [dbo].[Customer]";
            var dynamicParameters = new DynamicParameters();
            if (id != 0) {
                query += " WHERE customer_id = @customer_id";
                dynamicParameters.Add("customer_id", id);
            }
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"))) {
                var customers = await connection.QueryFirstOrDefaultAsync<Customer>(query, dynamicParameters);
                return customers;
            };
        }

        public Task<int> Insert(Customer entity) {
            throw new NotImplementedException();
        }

        public Task<int> Update(Customer entity) {
            throw new NotImplementedException();
        }
    }
}

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


        // Select all customers
        public async Task<IReadOnlyList<Customer>> GetAll() {
            var query = @"SELECT *
                            FROM [dbo].[Customer]";
            var dynamicParameters = new DynamicParameters();

            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                var customers = await connection.QueryAsync<Customer>(query);
                return (IReadOnlyList<Customer>)customers;
            };
        }

        // Select customer by id
        public async Task<Customer> GetById(int id) {
            var query = @"SELECT *
                            FROM [dbo].[Customer]";
            var dynamicParameters = new DynamicParameters();
            if (id != 0) {
                query += " WHERE customerId = @CustomerId";
                dynamicParameters.Add("CustomerId", id);
            }
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"))) {
                var customers = await connection.QueryFirstOrDefaultAsync<Customer>(query, dynamicParameters);
                return customers;
            };
        }

        // Insert new customer
        public async Task<int> Insert(Customer entity)
        {
            var query = @"INSERT INTO [dbo].[Customer](
                            FirstName
                           ,LastName
                           ,Email
                           ,Password
                           ,Gender
                           ,Country
                           ,PhoneNumber)
                          VALUES
                            (@FirstName
                           ,@LastName
                           ,@Email
                           ,@Password
                           ,@Gender
                           ,@Country
                           ,@PhoneNumber);";

            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                var affectedRows = await connection.ExecuteAsync(query, entity);
                return affectedRows;
            }
        }

        // Edit customer
        public async Task<int> Update(Customer entity) {
            var query = @"UPDATE [dbo].[Customer] SET
                            FirstName = @FirstName
                           ,LastName = @LastName
                           ,Email = @Email
                           ,Password = @Password
                           ,Gender = @Gender
                           ,Country = @Country
                           ,PhoneNumber = @PhoneNumber
                          WHERE CustomerId = @CustomerId;";

            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                var affectedRows = await connection.ExecuteAsync(query, entity);
                return affectedRows;
            }
        }

        // Delete customer by id
        public async Task<int> Delete(int id)
        {
            var query = @"DELETE FROM [dbo].[Customer] WHERE CustomerId=@CustomerId;";

            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                var affectedRows = await connection.ExecuteAsync(query, new { CustomerId = id });
                return affectedRows;
            }
        }
    }
}

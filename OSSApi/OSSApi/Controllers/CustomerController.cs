using Microsoft.AspNetCore.Mvc;
using OSSApi.Models;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;


namespace OSSApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase {

        private const string connectionString = "Data Source=ITSPT-2NRHTQ2;Initial Catalog=OnlineShoppingSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        // get by id
        // TODO more search parameters
        [HttpGet]
        public async Task<IActionResult> GetCustomer(int id) {
            var query = @"SELECT *
                            FROM [dbo].[Customer]";
            var dynamicParameters = new DynamicParameters();
            if (id != 0) {
                query += " WHERE customer_id = @customer_id";
                dynamicParameters.Add("customer_id", id);
            }
            using (var connection = new SqlConnection(connectionString)) {
                var customers = await connection.QueryAsync<Customer>(query, dynamicParameters);
                return Ok(customers);
            };
        }

        // post request 
        // inserts a costumer with from json input
        [HttpPost]
        public async Task<IActionResult> InsertCustomer(Customer customer) {
            var query = @"INSERT INTO [dbo].[Customer]
                                ([first_name]
                                ,[last_name]
                                ,[email]
                                ,[password]
                                ,[gender]
                                ,[country]
                                ,[phone_number])
                            VALUES (@first_name, @last_name, @email, 
                                    @password, @gender, @country, @phone_number)";

            using (var connection = new SqlConnection(connectionString)) {
                await connection.ExecuteAsync(query, new {
                    @first_name = customer.first_name,
                    @last_name = customer.last_name,
                    @email = customer.email,
                    @password = customer.password,
                    @gender = customer.gender,
                    @country = customer.country,
                    @phone_number = customer.phone_number
                });
                return Ok();
            }
        }

        // put request to update customer
        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(Customer customer) {
            if (!ModelState.IsValid) {
                return BadRequest("Not a valid customer!");
            }

            var query = @"UPDATE [dbo].[Customer]
                              SET [first_name] = @first_name
                                 ,[last_name] = @last_name
                                 ,[email] = @email
                                 ,[password] = @password
                                 ,[gender] = @gender
                                 ,[country] = @country
                                 ,[phone_number] = @phone_number
                            WHERE customer_id=@customer_id";

            using (var connection = new SqlConnection(connectionString)) {
                await connection.ExecuteAsync(query, new {
                    @customer_id = customer.customer_id,
                    @first_name = customer.first_name,
                    @last_name = customer.last_name,
                    @email = customer.email,
                    @password = customer.password,
                    @gender = customer.gender,
                    @country = customer.country,
                    @phone_number = customer.phone_number
                });
                return Ok();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCostumer(int id) {

            var query = @"DELETE FROM [dbo].[Customer]
                            WHERE customer_id=@customer_id";

            using (var connection = new SqlConnection(connectionString)) {
                await connection.ExecuteAsync(query, new { customer_id = id });
                return Ok();
            }
        }

    }
}

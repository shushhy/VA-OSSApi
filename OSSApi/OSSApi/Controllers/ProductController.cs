using Microsoft.AspNetCore.Mvc;
using OSSApi.Models;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace OSSApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase {
        private const string connectionString = "Data Source=ITSPT-6MRHTQ2;Initial Catalog=OnlineShoppingSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        [HttpGet]
        public async Task<IActionResult> GetProduct(int id) {
            var query = @"SELECT *
                            FROM [dbo].[Product]";
            var dynamicParameters = new DynamicParameters();
            if (id != 0) {
                query += " WHERE product_id = @product_id";
                dynamicParameters.Add("product_id", id);
            }
            using (var connection = new SqlConnection(connectionString)) {
                var products = await connection.QueryAsync<Customer>(query, dynamicParameters);
                return Ok(products);
            };
        }

        // post request 
        // inserts a product with from json input
        [HttpPost]
        public async Task<IActionResult> InsertProduct(Product product) {
            var query = @"INSERT INTO [dbo].[Product]
                                  ([product_name]
                                  ,[product_price]
                                  ,[product_size]
                                  ,[product_color]
                                  ,[product_description])
                            VALUES
                                  (@product_name,
                                  ,@product_price
                                  ,@product_size
                                  ,@product_color
                                  ,@product_description)";

            using (var connection = new SqlConnection(connectionString)) {
                await connection.ExecuteAsync(query, new {
                    @product_name = product.product_name,
                    @product_price = product.product_price,
                    @product_size = product.product_size,
                    @product_color = product.product_color,
                    @product_description = product.product_description
                });
                return Ok();
            }
        }

        // put request to update product
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Product product) {
            if (!ModelState.IsValid) {
                return BadRequest("Not a valid product!");
            }

            var query = @"UPDATE [dbo].[Product]
                              SET [product_name]        = @product_name,
                                 ,[product_price]       = @product_price
                                 ,[product_size]        = @product_size
                                 ,[product_color]       = @product_color
                                 ,[product_description] = @product_description) 
                            WHERE product_id = @product_id";

            using (var connection = new SqlConnection(connectionString)) {
                await connection.ExecuteAsync(query, new {
                    @product_id = product.product_id,
                    @product_name = product.product_name,
                    @product_price = product.product_price,
                    @product_size = product.product_size,
                    @product_color = product.product_color,
                    product_description = product.product_description
                });
                return Ok();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id) {

            var query = @"DELETE FROM [dbo].[Product]
                            WHERE product_id=@product_id";

            using (var connection = new SqlConnection(connectionString)) {
                await connection.ExecuteAsync(query, new { product_id = id });
                return Ok();
            }
        }

    }
}

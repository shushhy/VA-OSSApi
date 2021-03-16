using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using OSSApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSSApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase {
        private const string connectionString = "Data Source=ITSPT-6MRHTQ2;Initial Catalog=OnlineShoppingSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        // GET
        [HttpGet("")]
        public async Task<IActionResult> GetOrders(int id)
        {
            var sql = @"SELECT [order_id]
                              ,[customer_id]
                              ,[order_details]
                              ,[order_status]
                              ,[order_date]
                          FROM [dbo].[Orders]";

            var dynamicParameters = new DynamicParameters();
            if (id != 0)
            {
                sql += " WHERE order_id = @order_id";
                dynamicParameters.Add("order_id", id);
            }

            using (var connection = new SqlConnection(connectionString))
            {
                var orders = await connection.QueryAsync<Orders>(sql, dynamicParameters);
                return Ok(orders);
            };
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> InsertOrders(Orders orders)
        {
            var sql = @"INSERT INTO [dbo].[Orders]
                                ([customer_id]
                                ,[order_details]
                                ,[order_status]
                                ,[order_date])
                            VALUES (@customer_id, @order_details, @order_status, @order_date)";

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sql, new
                {
                    @customer_id = orders.customer_id,
                    @order_details = orders.order_details,
                    @order_status = orders.order_status,
                    @order_date = orders.order_date
                });
                return Ok();
            }
        }

        // PUT
        [HttpPut]
        public async Task<IActionResult> UpdateOrders(Orders orders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid order!");
            }

            var sql = @"UPDATE [dbo].[Orders]
                              SET [customer_id] = @customer_id
                                 ,[order_details] = @order_details
                                 ,[order_status] = @order_status
                                 ,[order_date] = @order_date
                            WHERE order_id=@order_id";

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sql, new
                {
                    @order_id = orders.order_id,
                    @customer_id = orders.customer_id, 
                    @order_details = orders.order_details,
                    @order_status = orders.order_status,
                    @order_date = orders.order_date
                });
                return Ok();
            }
        }

        // DELETE
        [HttpDelete]
        public async Task<IActionResult> DeleteOrders(int id)
        {

            var sql = @"DELETE FROM [dbo].[Orders]
                            WHERE order_id=@order_id";

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sql, new { order_id = id });
                return Ok();
            }
        }
    }
}

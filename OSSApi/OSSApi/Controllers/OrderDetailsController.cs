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
    public class OrderDetailsController : ControllerBase {
        private const string connectionString = "Data Source=ITSPT-6MRHTQ2;Initial Catalog=OnlineShoppingSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        // GET
        [HttpGet("")]
        public async Task<IActionResult> GetOrderDetails(int id)
        {
            var sql = @"SELECT [order_details_id]
                              ,[product_id]
                              ,[order_id]
                              ,[order_details_quantity]
                              ,[order_details_price]
                          FROM [dbo].[Order_details]";

            var dynamicParameters = new DynamicParameters();
            if (id != 0)
            {
                sql += " WHERE order_details_id = @order_details_id";
                dynamicParameters.Add("order_details_id", id);
            }

            using (var connection = new SqlConnection(connectionString))
            {
                var orderdetails = await connection.QueryAsync<Order_Details>(sql, dynamicParameters);
                return Ok(orderdetails);
            };
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> InsertOrderDetails(Order_Details order_Details)
        {
            var sql = @"INSERT INTO [dbo].[Order_details]
                                ([product_id]
                                ,[order_id]
                                ,[order_details_quantity]
                                ,[order_details_price])
                            VALUES (@product_id, @order_id, @order_details_quantity, @order_details_price)";

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sql, new
                {
                    @product_id = order_Details.product_id,
                    @order_id = order_Details.order_id,
                    @order_details_quantity = order_Details.order_details_quantity,
                    @order_details_price = order_Details.order_details_price
                });
                return Ok();
            }
        }

        // PUT
        [HttpPut]
        public async Task<IActionResult> UpdateOrderDetails(Order_Details orders_details)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid order request!");
            }

            var sql = @"UPDATE [dbo].[Order_details]
                              SET [product_id] = @product_id
                                 ,[order_id] = @order_id
                                 ,[order_details_quantity] = @order_details_quantity
                                 ,[order_details_price] = @order_details_price
                            WHERE order_details_id=@order_details_id";

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sql, new
                {
                    @order_details_id = orders_details.order_details_id,
                    @product_id = orders_details.product_id,
                    @order_id = orders_details.order_id,
                    @order_details_quantity = orders_details.order_details_quantity,
                    @order_details_price = orders_details.order_details_price
                });
                return Ok();
            }
        }

        // DELETE
        [HttpDelete]
        public async Task<IActionResult> DeleteOrderDetails(int id)
        {

            var sql = @"DELETE FROM [dbo].[Order_details]
                            WHERE order_details_id=@order_details_id";

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sql, new { order_details_id = id });
                return Ok();
            }
        }
    }
}

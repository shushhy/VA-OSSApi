using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OSS.Core.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using Dapper.Contrib.Extensions;


namespace OSS.Data.Repository {
    public class OrderDetailsRepository : IOrderDetailsRepository {
        private readonly IConfiguration configuration;

        public OrderDetailsRepository(IConfiguration configuration) {
            this.configuration = configuration;
        }


        // Select all order details
        public async Task<IReadOnlyList<OrderDetails>> GetAllAsync() {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"))) {
                var orderDetails = await connection.GetAllAsync<OrderDetails>();
                return orderDetails.AsList();
            };
        }

        // Select order details by id
        public async Task<OrderDetails> GetByIdAsync(int id) {
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"))) {
                var orderDetails = await connection.GetAsync<OrderDetails>(new OrderDetails { OrderDetailsId = id });
                return orderDetails;
            }
        }

        // Insert new order details
        public async Task InsertAsync(OrderDetails orderDetails) {
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"))) {
                var product = await connection.GetAsync<Product>(new Product { ProductId = orderDetails.ProductId });
                var newTotal = product.ProductPrice * orderDetails.OrderDetailsQuantity;
                var newOrderDetails = new OrderDetails {
                    ProductId = orderDetails.ProductId,
                    OrderId = orderDetails.OrderId,
                    OrderDetailsQuantity = orderDetails.OrderDetailsQuantity,
                    OrderDetailsPrice = newTotal
                };
                await connection.InsertAsync<OrderDetails>(newOrderDetails);
            }
        }

        // Edit order details
        public async Task UpdateAsync(OrderDetails entity) {
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"))) {
                string query = "select ProductPrice from Products;";
                var product = await connection.QueryFirstOrDefaultAsync<Product>(query);
                var updateOrderDetails = new OrderDetails {
                    OrderDetailsId = entity.OrderDetailsId,
                    ProductId = entity.ProductId,
                    OrderId = entity.OrderId,
                    OrderDetailsQuantity = entity.OrderDetailsQuantity,
                    OrderDetailsPrice = entity.OrderDetailsQuantity * product.ProductPrice
                };
                await connection.UpdateAsync<OrderDetails>(updateOrderDetails);
            }
        }

        // Delete order details by id
        public Task DeleteAsync(int id) {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OSS.Core.Models;
using OSS.Data.Repository;
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
        public Task<OrderDetails> GetByIdAsync(int id) {
            throw new NotImplementedException();
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
        public Task UpdateAsync(OrderDetails entity) {
            throw new NotImplementedException();
        }

        // Delete order details by id
        public Task DeleteAsync(int id) {
            throw new NotImplementedException();
        }
    }
}

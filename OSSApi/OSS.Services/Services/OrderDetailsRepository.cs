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
    public class OrderDetailsRepository : IOrderDetailsRepository {
        private readonly IConfiguration configuration;

        public OrderDetailsRepository(IConfiguration configuration) {
            this.configuration = configuration;
        }


        // Select all order details
        public Task<IReadOnlyList<OrderDetails>> GetAll() {
            throw new NotImplementedException();
        }

        // Select order details by id
        public Task<OrderDetails> GetById(int id) {
            throw new NotImplementedException();
        }

        // Insert new order details
        public async Task Insert(OrderDetails entity) {
            var query = @"INSERT INTO [dbo].[OrderDetails](
                            [ProductId]
                           ,[OrderId]
                           ,[OrderDetailsQuantity]
                           ,[OrderDetailsPrice])
                          VALUES
                            (@ProductId
                           ,@OrderId
                           ,@OrderDetailsQuantity
                           ,@OrderDetailsPrice);";

            var query2 = @"SELECT ProductPrice FROM Product p inner join OrderDetails o on p.ProductId = o.ProductId;";

            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"))) {
                //var affectedRows = await connection.ExecuteAsync(query, entity);
                var affectedRows = await connection.ExecuteAsync(query, new {
                    @ProductId = entity.ProductId,
                    @OrderId = entity.OrderId,
                    @OrderDetailsQuantity = entity.OrderDetailsQuantity,
                    @OrderDetailsPrice = entity.OrderDetailsPrice
                });

            }
        }

        // Edit order details
        public Task Update(OrderDetails entity) {
            throw new NotImplementedException();
        }

        // Delete order details by id
        public Task Delete(int id) {
            throw new NotImplementedException();
        }
    }
}

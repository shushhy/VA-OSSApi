using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OSS.Core.Models;
using OSS.Data.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Linq;

namespace OSS.Services.Services {
    public class OrdersRepository : IOrdersRepository {
        private readonly IConfiguration configuration;

        public OrdersRepository(IConfiguration configuration) {
            this.configuration = configuration;
        }


        // Select all orders
        public async Task<IReadOnlyList<Orders>> GetAll() {
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                var orderAmount = new Dictionary<int, Orders>();

                var query = await connection.QueryAsync<Orders, OrderDetails, Orders>(@"SELECT * FROM Orders o inner join OrderDetails d on o.OrderId = d.OrderId",
                    (ordersFunc, orderDetailsFunc) => {
                        Orders ordersid;

                        // Verificar se existe mais do que um produto associado a uma compra e juntá-los
                        if(!orderAmount.TryGetValue(ordersFunc.OrderId, out ordersid))
                        {
                            ordersid = ordersFunc;
                            ordersid.OrderDetails = new List<OrderDetails>();
                            orderAmount.Add(ordersid.OrderId, ordersid);
                        }

                        ordersid.OrderDetails.Add(orderDetailsFunc);               

                        return ordersid;
                },splitOn: "OrderDetailsId");

                return query.Distinct().ToList();
            };
        }

        // Select order by id
        public Task<Orders> GetById(int id) {
            throw new NotImplementedException();
            /*var query = @"SELECT * FROM Orders o inner join OrderDetails d on o.OrderId = d.OrderId ";
            var dynamicParameters = new DynamicParameters();
            if (id != 0)
            {
                query += " WHERE OrderId = @OrderId";
                dynamicParameters.Add("OrderId", id);
            }
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                var orders = await connection.QueryFirstOrDefaultAsync<Orders>(query, dynamicParameters);
                return orders;
            };*/
        }

        // Insert new order
        public async Task<int> Insert(Orders entity)
        {
            var query = @"INSERT INTO [dbo].[Orders](CustomerId, OrderDescription, OrderStatus, OrderDate)
                          VALUES (@CustomerId, @OrderDescription, @OrderStatus, @OrderDate);";

            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                var newOrder = new Orders
                {
                    CustomerId = entity.CustomerId,
                    OrderDescription = entity.OrderDescription,
                    OrderStatus = entity.OrderStatus,
                    OrderDate = DateTime.UtcNow
                };

                var affectedRows = await connection.ExecuteAsync(query, newOrder);
                return affectedRows;
            }
        }

        // Edit order
        public Task<int> Update(Orders entity) {
            throw new NotImplementedException();
        }

        // Delete order
        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}

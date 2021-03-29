using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OSS.Core.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Linq;

namespace OSS.Data.Repository {
    public class OrdersRepository : IOrdersRepository {
        private readonly IConfiguration configuration;

        public OrdersRepository(IConfiguration configuration) {
            this.configuration = configuration;
        }


        // Select all orders
        public async Task<IReadOnlyList<Orders>> GetAllAsync() {
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"))) {
                var orderAmount = new Dictionary<int, Orders>();

                var query = await connection.QueryAsync<Orders, OrderDetails, Orders>(@"SELECT * FROM Orders o inner join OrderDetails d on o.OrderId = d.OrderId",
                    (ordersFunc, orderDetailsFunc) => {
                        Orders ordersid;

                        // Verificar se existe mais do que um produto associado a uma compra e juntá-los
                        if (!orderAmount.TryGetValue(ordersFunc.OrderId, out ordersid)) {
                            ordersid = ordersFunc;
                            ordersid.OrderDetails = new List<OrderDetails>();
                            orderAmount.Add(ordersid.OrderId, ordersid);
                        }

                        ordersid.OrderDetails.Add(orderDetailsFunc);

                        return ordersid;
                    }, splitOn: "OrderDetailsId");

                return query.Distinct().ToList();
            };
        }

        // Select order by id
        // TODO FIX 
        public async Task<Orders> GetByIdAsync(int id) {
            //throw new NotImplementedException();
            var query = @"SELECT * FROM Orders o inner join OrderDetails d on o.OrderId = d.OrderId ";
            var dynamicParameters = new DynamicParameters();
            if (id != 0) {
                query += " WHERE OrderId = @OrderId";
                dynamicParameters.Add("OrderId", id);
            }
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"))) {
                var orders = await connection.QueryFirstOrDefaultAsync<Orders>(query, dynamicParameters);
                return orders;
            };
        }

        // Insert new order
        public async Task InsertAsync(Orders orders) {
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"))) {
                var newOrder = new Orders {
                    CustomerId = orders.CustomerId,
                    OrderDescription = orders.OrderDescription,
                    OrderStatus = orders.OrderStatus,
                    OrderDate = DateTime.UtcNow
                };
                await connection.InsertAsync<Orders>(newOrder);
            }
        }

        // Edit order
        public async Task UpdateAsync(Orders orders) {
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"))) {
                var updateOrder = new Orders {
                    OrderId = orders.OrderId,
                    CustomerId = orders.CustomerId,
                    OrderDescription = orders.OrderDescription,
                    OrderStatus = orders.OrderStatus,
                    OrderDate = DateTime.UtcNow
                };
                await connection.InsertAsync<Orders>(updateOrder);
            }
        }

        // Delete order
        public async Task DeleteAsync(int id) {
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"))) {
                await connection.DeleteAsync(new Orders { OrderId = id });
            }
        }
    }
}

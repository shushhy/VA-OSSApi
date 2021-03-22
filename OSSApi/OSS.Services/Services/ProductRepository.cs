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
    public class ProductRepository : IProductRepository {
        private readonly IConfiguration configuration;

        public ProductRepository(IConfiguration configuration) {
            this.configuration = configuration;
        }


        // Select all products
        public async Task<IReadOnlyList<Product>> GetAll() {
            var query = @"SELECT *
                            FROM [dbo].[Product]";
            var dynamicParameters = new DynamicParameters();

            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                var products = await connection.QueryAsync<Product>(query);
                return (IReadOnlyList<Product>)products;
            };
        }
        
        // Select product by id
        public async Task<Product> GetById(int id) {
            var query = @"SELECT *
                            FROM [dbo].[Product]";
            var dynamicParameters = new DynamicParameters();
            if (id != 0) {
                query += " WHERE ProductId = @ProductId";
                dynamicParameters.Add("ProductId", id);
            }
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"))) {
                var products = await connection.QueryFirstOrDefaultAsync<Product>(query, dynamicParameters);
                return products;
            };
        }

        // Insert new product
        public async Task<int> Insert(Product entity)
        {
            var query = @"INSERT INTO [dbo].[Product](
                            ProductName
                           ,ProductPrice
                           ,ProductSize
                           ,ProductColor
                           ,ProductDescription)
                          VALUES
                            (@ProductName
                           ,@ProductPrice
                           ,@ProductSize
                           ,@ProductColor
                           ,@ProductDescription);";

            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                var affectedRows = await connection.ExecuteAsync(query, entity);
                return affectedRows;
            }
        }

        // Edit product
        public async Task<int> Update(Product entity) {
            var query = @"UPDATE [dbo].[Product] SET
                            ProductName = @ProductName
                           ,ProductPrice = @ProductPrice
                           ,ProductSize = @ProductSize
                           ,ProductColor = @ProductColor
                           ,ProductDescription = @ProductDescription
                          WHERE ProductId = @ProductId;";

            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                var affectedRows = await connection.ExecuteAsync(query, entity);
                return affectedRows;
            }
        }

        // Delete product by id
        public async Task<int> Delete(int id)
        {
            var query = @"DELETE FROM [dbo].[Product] WHERE ProductId=@ProductId;";

            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                var affectedRows = await connection.ExecuteAsync(query, new { ProductId = id });
                return affectedRows;
            }
        }
    }
}

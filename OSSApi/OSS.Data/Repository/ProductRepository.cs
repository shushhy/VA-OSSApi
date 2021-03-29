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
using System.Linq;

namespace OSS.Data.Repository {
    public class ProductRepository : IProductRepository {
        private readonly IConfiguration configuration;

        public ProductRepository(IConfiguration configuration) {
            this.configuration = configuration;
        }


        // Select all products
        public async Task<IReadOnlyList<Product>> GetAllAsync() {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"))) {
                var products = await connection.GetAllAsync<Product>();
                return products.ToList();
            };
        }

        // Select product by id
        public async Task<Product> GetByIdAsync(int id) {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"))) {
                int ProductId = id;
                var product = await connection.GetAsync<Product>(ProductId);
                return product;
            }
        }

        // Insert new product
        public async Task InsertAsync(Product product) {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"))) {
                var newProduct = new Product {
                    ProductName = product.ProductName,
                    ProductPrice = product.ProductPrice,
                    ProductSize = product.ProductSize,
                    ProductColor = product.ProductColor,
                    ProductDescription = product.ProductDescription
                };
                await connection.InsertAsync<Product>(newProduct);
            }
        }

        // Edit product
        public async Task UpdateAsync(Product product) {
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"))) {
                var updateProduct = new Product {
                    ProductName = product.ProductName,
                    ProductPrice = product.ProductPrice,
                    ProductSize = product.ProductSize,
                    ProductColor = product.ProductColor,
                    ProductDescription = product.ProductDescription
                };
                await connection.UpdateAsync<Product>(updateProduct);
            }
        }

        // Delete product by id
        public async Task DeleteAsync(int id) {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"))) {
                await connection.DeleteAsync(new Product { ProductId = id });
            }
        }

    }
}

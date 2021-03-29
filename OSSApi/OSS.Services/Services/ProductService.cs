using OSS.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using OSS.Data.Repository;
using Serilog;
using System.Text.Json;

namespace OSS.Services.Services {
    public class ProductService : IProductService {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository) {
            this.productRepository = productRepository;
        }

        public async Task DeleteAsync(int id) {
            await productRepository.DeleteAsync(id);
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync() {
            var data = await productRepository.GetAllAsync();
            if (data == null) {
                Log.Information("Failed to retrieve products");
                return null;
            }
            Log.Information("Retrieved products: {data}", JsonSerializer.Serialize(data));
            return data;
        }

        public async Task<Product> GetByIdAsync(int id) {
            var data = await productRepository.GetByIdAsync(id);
            if (data == null) {
                Log.Information("Failed to retrieve product, id: {id}", id);
                return null;
            }
            return data;
        }

        public async Task InsertAsync(Product entity) {
            await productRepository.InsertAsync(entity);
        }

        public async Task UpdateAsync(Product entity) {
            await productRepository.UpdateAsync(entity);
        }
    }
}

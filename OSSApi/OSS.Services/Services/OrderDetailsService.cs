using OSS.Core.Models;
using System;
using System.Collections.Generic;
using Serilog;
using System.Threading.Tasks;
using OSS.Data.Repository;
using System.Text.Json;

namespace OSS.Services.Services {
    public class OrderDetailsService : IOrderDetailsService {
        private readonly IOrderDetailsRepository orderDetailsRepository;
        public async Task DeleteAsync(int id) {
            await orderDetailsRepository.DeleteAsync(id);
        }

        public async Task<IReadOnlyList<OrderDetails>> GetAllAsync() {
            var data = await orderDetailsRepository.GetAllAsync();
            if (data == null) {
                Log.Information("Failed to retrieve OrderDetails");
                return null;
            }
            Log.Information("Retrieved OrderDetails: {data}", JsonSerializer.Serialize(data));
            return data;
        }

        public async Task<OrderDetails> GetByIdAsync(int id) {
            var data = await orderDetailsRepository.GetByIdAsync(id);
            if (data == null) {
                Log.Information("Failed to retrieve OrderDetails, id: {id}", id);
                return null;
            }
            Log.Information("Retrieved OrderDetails: {data}", JsonSerializer.Serialize(data));
            return data;
        }

        public async Task InsertAsync(OrderDetails entity) {
            await orderDetailsRepository.InsertAsync(entity);
            Log.Information("Inserted OrderDetails: {data}", JsonSerializer.Serialize(entity));
        }

        public async Task UpdateAsync(OrderDetails entity) {
            await orderDetailsRepository.UpdateAsync(entity);
            Log.Information("Updated OrderDetails: {data}", JsonSerializer.Serialize(entity));
        }
    }
}

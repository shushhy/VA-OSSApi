using OSS.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;
using OSS.Data.Repository;
using System.Text.Json;

namespace OSS.Services.Services {
    public class OrdersService : IOrdersService {
        private readonly IOrdersRepository ordersRepository;

        public OrdersService(IOrdersRepository ordersRepository) {
            this.ordersRepository = ordersRepository;
        }

        public async Task DeleteAsync(int id) {
            await ordersRepository.DeleteAsync(id);
        }

        public async Task<IReadOnlyList<Orders>> GetAllAsync() {
            var data = await ordersRepository.GetAllAsync();
            if (data == null) {
                Log.Information("Failed to retrieve Orders");
                return null;
            }
            return data;
        }

        public async Task<Orders> GetByIdAsync(int id) {
            var data = await ordersRepository.GetByIdAsync(id);
            if (data == null) {
                Log.Information("Failed to retrieve Orders, id: {id}", id);
                return null;
            }
            return data;
        }

        public async Task InsertAsync(Orders entity) {
            await ordersRepository.InsertAsync(entity);
            Log.Information("Inserted Orders: {data}", JsonSerializer.Serialize(entity));
        }

        public async Task UpdateAsync(Orders entity) {
            await ordersRepository.UpdateAsync(entity);
            Log.Information("Inserted Orders: {data}", JsonSerializer.Serialize(entity));
        }
    }
}

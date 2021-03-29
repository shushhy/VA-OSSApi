using System.Collections.Generic;
using System.Threading.Tasks;
using OSS.Core.Models;
using OSS.Data.Repository;
using Serilog;
using System.Text.Json;

namespace OSS.Services.Services {
    public class CustomerService : ICustomerService {

        private readonly ICustomerRepository customerRepository;

        public CustomerService(ICustomerRepository customerRepository) {
            this.customerRepository = customerRepository;
        }

        public async Task DeleteAsync(int id) {
            await customerRepository.DeleteAsync(id);
        }

        public async Task<IReadOnlyList<Customer>> GetAllAsync() {
            var data = await customerRepository.GetAllAsync();
            if (data == null) {
                Log.Information("Failed to retrieve customers");
                return null;
            }
            Log.Information("Retrieved Customers: {data}", JsonSerializer.Serialize(data));
            return data;
        }

        public async Task<Customer> GetByIdAsync(int id) {
            var data = await customerRepository.GetByIdAsync(id);
            if (data == null) {
                Log.Information("Failed to retrieve Customer, id: {id}", id);
                return null;
            }
            Log.Information("Retrieved Customer: {data}", JsonSerializer.Serialize(data));
            return data;
        }

        public async Task InsertAsync(Customer entity) {
            await customerRepository.InsertAsync(entity);
            Log.Information("Inserted Customer: {data}", JsonSerializer.Serialize(entity));
        }

        public async Task UpdateAsync(Customer entity) {
            await customerRepository.UpdateAsync(entity);
            Log.Information("Updated Customer: {data}", JsonSerializer.Serialize(entity));
        }
    }
}

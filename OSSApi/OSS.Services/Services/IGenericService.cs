using System.Collections.Generic;
using System.Threading.Tasks;

namespace OSS.Services.Services {
    public interface IGenericService<T> where T : class {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}

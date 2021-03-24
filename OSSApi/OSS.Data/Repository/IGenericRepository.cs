using System.Collections.Generic;
using System.Threading.Tasks;

namespace OSS.Data.Repository {
    public interface IGenericRepository<T> where T : class {
        Task<IReadOnlyList<T>> GetAll();
        Task<T> GetById(int id);
        Task Insert(T entity);
        Task Update(T entity);
        Task Delete(int id);
    }
}

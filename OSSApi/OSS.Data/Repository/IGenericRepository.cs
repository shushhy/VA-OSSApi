using System.Collections.Generic;
using System.Threading.Tasks;

namespace OSS.Data.Repository {
    public interface IGenericRepository<T> where T : class {
        Task<IReadOnlyList<T>> GetAll();
        Task<T> GetById(int id);
        Task<int> Insert(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(int id);
    }
}

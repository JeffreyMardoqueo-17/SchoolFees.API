using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolFees.DAL.Interfaces
{
    public interface ICrudRepository<T> where T : class
    {
        Task<T> CreateAsync(T entity);
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}

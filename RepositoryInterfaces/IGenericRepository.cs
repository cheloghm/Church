using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Church.RepositoryInterfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> GetByDate(DateTime date);
        Task<T> GetByIdAsync(string id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity, string id);
        Task DeleteAsync(string id);
        Task<IEnumerable<T>> SearchAsync(string query);
    }
}

using Church.Repositories;
using Church.RepositoryInterfaces;
using Church.ServiceInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Church.Services
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;

        public GenericService(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<T> AddAsync(T entity)
        {
            return await _repository.AddAsync(entity);
        }

        public IEnumerable<T> GetByDate(DateTime date)
        {
            return _repository.GetByDate(date);
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<T> UpdateAsync(T entity, string id)
        {
            return await _repository.UpdateAsync(entity, id);
        }

        public async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<T>> SearchAsync(string query)
        {
            return await _repository.SearchAsync(query);
        }

    }
}

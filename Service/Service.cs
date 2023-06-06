
using IRepository;
using IService;

namespace Service
{
    public class Service<T> : IService<T>
    {

        public readonly IRepository<T> Repository;
        public Service(IRepository<T> repository) { 
            this.Repository = repository;
        }
          public async Task<bool> AddAsync(T entity)
        {
             return  await  Repository.AddAsync(entity);
        }

         public Task DeleteAsync(T entity)
        {
             return Repository.DeleteAsync(entity);
        }

        public Task<T> GetByIdAsync(int id)
        {
             return Repository.GetByIdAsync(id);
        }

        public Task UpdateAsync(T entity)
        {
            return Repository.UpdateAsync(entity);
        }
    }
}
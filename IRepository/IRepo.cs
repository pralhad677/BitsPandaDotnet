namespace IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> getAll();
        Task<T> GetByIdAsync(int id);
        Task<bool> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
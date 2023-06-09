using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService
{
    public interface IAdminService<T>
    {
        Task<List<T>> getAll();
        Task<T> GetByIdAsync(int id);
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(Guid Id, string Username);
        Task<bool> DeleteAsync(Guid Id);
    }
}

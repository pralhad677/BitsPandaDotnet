using IRepository;
using IService;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AdminService<T> : IAdminService<T> where T : class
    {
        public readonly IAdminRepo<T> _adminRepo;
        public AdminService(IAdminRepo<T> adminRepo)
        {
            this._adminRepo = adminRepo;
        }
       
        public Task<bool> AddAsync(T entity)
        {
             return _adminRepo.AddAsync(entity);
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> getAll()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}

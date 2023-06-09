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
       
       async public Task<bool> AddAsync(T entity)
        {
             return await _adminRepo.AddAsync(entity);
        }

         

       async public Task<bool> DeleteAsync( Guid Id)
        {
             return await _adminRepo.DeleteAsync(Id);
        }

        async public Task<List<T>> getAll()
        {
            return await _adminRepo.getAll();
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

       async public Task<bool> UpdateAsync(Guid Id, string Username)
        {
           return await _adminRepo.UpdateAsync(Id, Username);
        }
    }
}

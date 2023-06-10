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

      async  public Task<List<T>> GetByIdAsync(Guid id)
        {
             return await _adminRepo.GetByIdAsync(id);
        }

       async public Task<bool> UpdateAsync(Guid Id, string Username)
        {
           return await _adminRepo.UpdateAsync(Id, Username);
        }

        async public Task<string> LogIn(string Username, string Password)
        {
            return await _adminRepo.LogIn(Username, Password);
        }
        async public Task<bool> UserExist(string Username)
        {
            return await _adminRepo.UserExist(Username);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService
{
    public interface IAdminService<T>
    {
        Task<List<T>> getAll();
        Task<List<T>> GetByIdAsync(Guid id);
        Task<bool> AddAsync(T entity);
        Task<object> UpdateAsync(Guid Id, string Username);
        Task<bool> DeleteAsync(Guid Id);

        Task<string> LogIn(string Username, string Password);
        Task<bool> UserExist(string Username);
    }
}

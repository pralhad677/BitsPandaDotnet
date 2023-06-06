using IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly IDbContextFactory<MyDbContext> _dbContextFactory;
        public Repository(IDbContextFactory<MyDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
       async public Task<bool> AddAsync(T entity)
        {
            using var dbContext = await _dbContextFactory.CreateDbContextAsync();
               var x = dbContext.Set<T>().Add(entity);
            await dbContext.SaveChangesAsync();
            bool result;
             
            result = x !=null? true : false;
            return result;
           
        } 

       async public Task DeleteAsync(T entity)
        {
            using var dbContext = await _dbContextFactory.CreateDbContextAsync();
            throw new NotImplementedException();
        }

       async public Task<T> GetByIdAsync(int id)
        {
            using var dbContext = await _dbContextFactory.CreateDbContextAsync();
            throw new NotImplementedException();
        }

       async public Task UpdateAsync(T entity)
        {
            using var dbContext = await _dbContextFactory.CreateDbContextAsync();
            throw new NotImplementedException();
        }
    }
}

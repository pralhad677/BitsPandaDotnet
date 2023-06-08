using IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;

namespace Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly MyDbContext _dbContextFactory;
        public Repository(MyDbContext dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
       async public Task<bool> AddAsync(T entity)
        { 
               var x = _dbContextFactory.Set<T>().Add(entity);
            await _dbContextFactory.SaveChangesAsync();
            bool result;
             
            result = x !=null? true : false;
            return result;
           
        } 

       async public Task DeleteAsync(T entity)
        {
            //using var dbContext = await _dbContextFactory.CreateDbContextAsync();
            throw new NotImplementedException();
        }
        

        [DbFunction("public", "getAll")]
        async public Task<List<T>> getAll()
        {
             
           
            var x = await _dbContextFactory.Set<T>().FromSqlInterpolated($"SELECT * FROM get_all_users()").ToListAsync();
            
            return x;
        }

        async public Task<T> GetByIdAsync(int id)
        {
            //using var dbContext = await _dbContextFactory.CreateDbContextAsync();
            throw new NotImplementedException();
        }

       async public Task UpdateAsync(T entity)
        {
            //using var dbContext = await _dbContextFactory.CreateDbContextAsync();
            throw new NotImplementedException();
        }
    }
}

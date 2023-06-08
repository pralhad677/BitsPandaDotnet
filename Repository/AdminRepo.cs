using IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Repository
{
    public class AdminRepo<T> : IAdminRepo<T> where T : class
    {
        protected readonly MyDbContext _dbContextFactory;
        public AdminRepo(MyDbContext dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public Task<bool> AddAsync(T entity)
        {
            var hasPasswordProperty = typeof(T).GetProperty("Password") != null;
            var isConfirmMatched = typeof(T).GetProperty("ConfirmPassword") != null;
            var h = new Hash();
            string password="";
            string confirmPassword = "";
            if (hasPasswordProperty)
            {
                // The type T has a Password property
                // You can access the Password property of the entity here
                var  passwordValue = (string)entity.GetType().GetProperty("Password").GetValue(entity).ToString();
               
                
                    password = passwordValue;
                 

                // Rest of your logic...
            }
            if (isConfirmMatched)
            {
                var ConfirmpasswordValue = (string)entity.GetType().GetProperty("ConfirmPassword").GetValue(entity).ToString();


                confirmPassword = ConfirmpasswordValue;
            }
            
            var hashedPassword = h.HashAndSetPassword(password);
            
            var matched = h.IsPasswordConfirmed(hashedPassword, confirmPassword) ;
            
                    
            
            
            throw new NotImplementedException();
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

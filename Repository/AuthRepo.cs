using IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AuthRepo : IAuthRepo
    {
        protected readonly MyDbContext _dbContextFactory;
        public AuthRepo(MyDbContext dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public Task<string> Login(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<int> Register(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserExist(string email)
        {
            throw new NotImplementedException();
        }
    }
}

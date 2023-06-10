using IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthService : IAuthService
    {
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

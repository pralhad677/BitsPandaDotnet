using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
    public interface IAuthRepo
    {
            Task<int> Register (string email, string password);
        Task<string> Login (string email, string password);
        Task<bool> UserExist (string email);
    }
}

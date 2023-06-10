using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
using System.Security.Cryptography;
 
namespace Repository
{
    public  class Hash
    {
        public string HashAndSetPassword(string Password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(Password));
            Password = Convert.ToHexString(hashedBytes);
            return Password;
        }

        public bool IsPasswordConfirmed(string Password, string ConfirmPassword)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(ConfirmPassword));
            var hashedConfirmPassword = Convert.ToHexString(hashedBytes);

            var istrue = Password.Equals(hashedConfirmPassword);
            if(ConfirmPassword.Contains(Password))
            {
                return true;
            }
            return istrue;
        }

    }
}


     
using IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Model;
using System.Data;
using Microsoft.Data.SqlClient; 
using System.Reflection;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;

namespace Repository
{
    public class AdminRepo<T> : IAdminRepo<T> where T : class
    {
        protected readonly MyDbContext _dbContextFactory;
        private readonly IConfiguration configuration;

        private readonly IHttpContextAccessor _httpContextAccessor;
        public AdminRepo(MyDbContext dbContextFactory,IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _dbContextFactory = dbContextFactory;
            this.configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
       async public Task<bool> AddAsync(T entity)
        {

            var hasPasswordProperty = typeof(T).GetProperty("Password") != null;
            var pw = "";
            if (hasPasswordProperty)
            {
               
                var passwordValue = entity.GetType().GetProperty("Password").GetValue(entity);
                pw = passwordValue.ToString();
            }
            var hasUsernameProperty = typeof(T).GetProperty("Username") != null;
            var Username = "";
            if (hasUsernameProperty)
            {

                var UsenameVale = entity.GetType().GetProperty("Username").GetValue(entity);
                Username = UsenameVale.ToString();
            }
            var Id = Guid.NewGuid();
            var idParam = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            idParam.Value = Id;

            var usernameParam = new SqlParameter("@Username", SqlDbType.VarChar, 50);
            usernameParam.Value = Username;

            var passwordParam = new SqlParameter("@Password", SqlDbType.VarChar, 50);
            passwordParam.Value = pw;

            //var databaseFacade =_dbContextFactory.GetService<DatabaseFacade>();
            // databaseFacade.ExecuteSqlRaw("EXEC InsertAdmin @Id, @Username, @Password",
            //     idParam, usernameParam, passwordParam);
          var data = await _dbContextFactory.Set<T>().FromSqlInterpolated($"EXEC InsertAdmin @Id={Id}, @Username={Username}, @Password={pw}").ToListAsync();
       //await _dbContextFactory.Set<T>().FromSqlInterpolated($"EXEC InsertAdmin @Id={Id}, @Username={Username}, @Password={pw}").ToListAsync();
          
            //var dbContext = _dbContextFactory.Set<T>();
            //await dbContext.FromSqlInterpolated($"EXEC InsertAdmin @Id, @Username, @Password", idParam, usernameParam, passwordParam).ToListAsync();


            //.ExecuteSqlRawAsync("EXEC InsertAdmin @Id, @Username, @Password",
            //idParam, usernameParam, passwordParam);


            //var databaseFacade = _dbContextFactory.Database.GetService<DatabaseFacade>();
            //await databaseFacade.ExecuteSqlRawAsync("EXEC InsertAdmin @Id, @Username, @Password", idParam, usernameParam, passwordParam);
             if(data.Count >0)
            {
                return true;
            }
            return false;
        }

       async public Task<bool> DeleteAsync(Guid Id)
        {
            //.FromSqlInterpolated($"EXEC InsertAdmin @Id={Id}, @Username={Username}, @Password={pw}")
             await _dbContextFactory.Set<T>().FromSqlInterpolated($"EXEC DeleteAdminById @Id={Id}").ToListAsync();
                Console.WriteLine("asd");
            return true;
        }

        async public Task<List<T>> getAll()
        {
            var data = await _dbContextFactory.Set<T>().FromSqlInterpolated($"EXEC  GetAdmins").ToListAsync();
            Console.WriteLine("asd");
            var updatedList = data.Select(user =>
            {
                if ((user as dynamic).Password.Length > 10)
                {
                    (user as dynamic).Password = (user as dynamic).Password.Substring(0, 10);
                }
                return user;
            }).ToList();
            //return data;
            return updatedList;


        }

        async public Task<List<T>> GetByIdAsync(Guid id)
        {
             var data =await _dbContextFactory.Set<T>().FromSqlInterpolated($"EXEC GetAdminById @Id={id}").ToListAsync();
            //PropertyInfo property = data[0].GetType().GetProperty("Username");
            //if (property != null && property.CanWrite)
            //{
            //    string originalUsername = property.GetValue(data) as string;
            //    property.SetValue(data, originalUsername.Replace("\\", ""));
            //}
            //data.ForEach(item => (item as dynamic).username = (item as dynamic).username.Replace("\\", ""));
            //int id1 = int.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c=>c.Type==ClaimTypes.NameIdentifier)!.Value);
            var claimsPrincipal = _httpContextAccessor.HttpContext.User;
            var isAuthenticated = _httpContextAccessor.HttpContext.User.Identity!.IsAuthenticated;
            var a = ClaimTypes.NameIdentifier;
            var idClaim = claimsPrincipal.FindFirst(ClaimTypes.Name);
            if (idClaim != null)
            {
                var id2 = idClaim.Value;
                Console.WriteLine(id2);
                // Access the id value as needed
            }

            return data;
        }

        async public Task<object> UpdateAsync(Guid Id, string Username)
        {
            if (await UserExist(Username))
            {
                return $"user with given {Username} already exist";
                
            }
             await _dbContextFactory.Set<T>().FromSqlInterpolated($"EXEC UpdateAdminNameById @Id={Id}, @NewUserName={Username}").ToListAsync();
            var data = await _dbContextFactory.Set<T>().FromSqlInterpolated($"EXEC GetAdminById @Id={Id}").ToListAsync();
            //(data as dynamic).Password=(data as dynamic).Password.Substring(0, 10);
            //Console.WriteLine(data);
            return data;
        }

       async public Task<string> LogIn(string Username, string Password)
        {
 
            
             var data = await _dbContextFactory.Set<T>().FromSqlInterpolated($"EXEC GetAdminByUsername @Username={Username}").ToListAsync();
            if (data.Count==0) {
                return $"user with {Username} doesnot exist";
            }
            var item = data[0];
 
            var UName = (item as dynamic).Username;
            var Pw = (item as dynamic).Password;
            var Id = (item as dynamic).Id;
            var h = new Hash();
            var passwordMatched = h.IsPasswordConfirmed(Pw, Password);
            if (h.IsPasswordConfirmed( Pw,Password))
            {
                var token = createToken(Id,UName);
                return token;
            }
            return "password do not match";
         }
        async public Task<bool> UserExist(string Username)
        {
        
            bool userExists;

            var usernameParam = new SqlParameter("@Username", Username);
            var userExistsParam = new SqlParameter("@UserExists", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Output
            };

            await _dbContextFactory.Database.ExecuteSqlInterpolatedAsync($@"
    EXEC CheckAdminExistence
        @Username = {usernameParam},
        @UserExists = {userExistsParam} OUTPUT
");

            userExists = (bool)userExistsParam.Value;

            Console.WriteLine("User Exists: " + userExists);
            return userExists;
        }

        private string  createToken(Guid Id,string Username)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,Id.ToString()),
                new Claim(ClaimTypes.Name,Username.ToString())
            };
            var key = configuration.GetSection("AppSettings:Token").Value;
            if(key == null)
            {
                throw new Exception("secret key is not provided");
            }
            SymmetricSecurityKey ssk = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key));
            SigningCredentials creds = new SigningCredentials(ssk, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires= DateTime.Now.AddMinutes(30),
                SigningCredentials =  creds
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();   
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

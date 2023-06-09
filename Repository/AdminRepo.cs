using IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Model;

namespace Repository
{
    public class AdminRepo<T> : IAdminRepo<T> where T : class
    {
        protected readonly MyDbContext _dbContextFactory;
        public AdminRepo(MyDbContext dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
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
            if (hasPasswordProperty)
            {

                var UsenameVale = entity.GetType().GetProperty("Username").GetValue(entity);
                Username = UsenameVale.ToString();
            }
            var Id = Guid.NewGuid();

            string query = "SELECT * FROM Admins WHERE Username = '{0}'";
            string username = "pralhad";
            string formattedQuery = string.Format(query, username);
            var x = await _dbContextFactory.Set<T>().FromSqlRaw<T>(formattedQuery).ToListAsync();
            //List<T> list= await _dbContextFactory.Set<T>().FromSqlRaw<T>($"SELECT * from  insert_admin('{Id.ToString()}', '{Username}','{pw}')").ToListAsync();


            throw new NotImplementedException();
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> getAll()
        {
            string connectionString = "Server=localhost; Database=bitspanda01; Port=5432; User Id=postgres; Password=pralhad;";
            List<T>  adminList = new List<T>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = @"SELECT * FROM public.""Admins""
                             ORDER BY ""Id"" ASC";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Access the columns by their respective names or indexes
                            var admin = new Admins.Admin();
                            var id = reader.GetGuid(reader.GetOrdinal("Id"));
                            string username = reader.GetString(reader.GetOrdinal("Username"));
                            string Password = reader.GetString(reader.GetOrdinal("Password"));

                            Console.WriteLine($"Id: {id}, Username: {username}, Email: {Password}");
                            admin.Id = id;
                            admin.Username=username;
                            admin.Password = Password;
                            adminList.Add(admin as T);
                        }
                    }
                }
            }
            
            var x = Task.Run(() =>
            {

                return adminList;
            });
            return x;
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

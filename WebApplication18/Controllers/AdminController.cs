using AutoMapper;
using DTO;
using IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;
using System.Security.Claims;

namespace WebApplication18.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class AdminController : ControllerBase
    {
        public readonly IAdminService<Admins.Admin> service;
        private readonly IMapper _mapper;

        public AdminController(IAdminService<Admins.Admin> service, IMapper mapper)
        {
            this.service = service;
            this._mapper = mapper;
        }
        [HttpPost("SignUp")]
        async public Task<ServiceResponse<bool>> SignUp(AdminDto adminDto)
        {

            var response = new ServiceResponse<bool>();

            var h = new Hash();
            var hashedPassword = h.HashAndSetPassword(adminDto.Password);
            var isMatched = h.IsPasswordConfirmed(hashedPassword, adminDto.ConfirmPassword);

            var admin = _mapper.Map<Admins.Admin>(adminDto);
            if (await service.UserExist(adminDto.Username))
            {
                response.IsSuccess = false;
                response.Errors = "user already exist";
                return response;
            }
            if (isMatched)
            {
                admin.Password = hashedPassword;
                var data = await service.AddAsync(admin);
                response.Data = data;
                response.IsSuccess = true;
                return response;
            }
            else {
                response.IsSuccess = false;
                response.Errors = "confirmPassword Do not Match";
                return response;
            }

        }
        //[Authorize]
        [HttpGet("getAll")]
        async public Task<ServiceResponse<dynamic>> getAll()
        {
            //Guid Id = User.Claims();
            //var idClaim = Admins.Admin.Find
            var x = await service.getAll();
            var response = new ServiceResponse<dynamic>();
            response.Data = x;
            response.Message = "OK";
            response.IsSuccess = true;
            response.IsSuccess = true;
            return response;
        }
        [HttpDelete("deleteByAdminId")]
        async public Task<ServiceResponse<bool>> deleteByAdminId([FromQuery] Guid Id)
        {

            var response = new ServiceResponse<bool>();
            response.IsSuccess = await service.DeleteAsync(Id);
            return response;
        }
        [HttpPatch("updateAdmin")]
        async public Task<ServiceResponse<object>> updateAdmin([FromQuery] string Id, string Username)
            {
               var response = new ServiceResponse<object>();
            Guid id;
            var istrue = Guid.TryParse(Id, out id);
            //response.Data = await service.UpdateAsync(id, Username);
            //response.IsSuccess = await service.UpdateAsync(id, Username) switch
            //{
            //    string message when message.Contains("already exist")=>false
            //};
            response.Data = await service.UpdateAsync(id, Username);
            response.IsSuccess = response.Data switch
            {
                string message when message.Contains("already exist") => false,
                _=>true
            };
            Console.WriteLine(response.IsSuccess);
            return response;

              }
        [HttpGet("gteById")]
        async public Task<ServiceResponse<List<Admins.Admin>>> getById([FromQuery]Guid Id)
        {
            var response = new ServiceResponse<List<Admins.Admin>>();
         var data=   await service.GetByIdAsync(Id);
            (data[0] as dynamic).Username = (data[0] as dynamic).Username.Replace("\"", "");
            response.Data = data as dynamic;
            return response;
        }

        [HttpPost("Login")]
        async public Task<ServiceResponse<bool>> Login(AdminDto admin)
        {
            var response = new ServiceResponse<bool>();
           string data = await service.LogIn(admin.Username, admin.Password);
            if (data.Contains("exist"))
            {
                response.IsSuccess = false;
                response.Message = data;
                return response;
            }
            else if (data.Contains("password"))
            {
                response.IsSuccess = false;
                response.Message = data;
                return response;
            }
            response.Message = data;           
            response.IsSuccess = true;
                
            return response;
        }
    }
}

using AutoMapper;
using DTO;
using IService;
using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;

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
        [HttpGet("getAll")]
        async public Task<ServiceResponse<dynamic>> getAll()
        {
            var x = await service.getAll();
            var response = new ServiceResponse<dynamic>();
            response.Data = x;
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
        async public Task<ServiceResponse<bool>> updateAdmin(Guid Id, string Username)
            {
               var response = new ServiceResponse<bool>();
        response.IsSuccess = await service.UpdateAsync(Id, Username);
            return response;

              }
           
            

    }
}

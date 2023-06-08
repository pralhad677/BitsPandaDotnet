using AutoMapper;
using DTO;
using IService;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace WebApplication18.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class AdminController:ControllerBase
    {
        public readonly IAdminService<Admins.Admin> service;
        private readonly IMapper _mapper;

        public AdminController(IAdminService<Admins.Admin> service, IMapper mapper)
        {
            this.service = service;
            this._mapper = mapper;
        }
        [HttpPost("SignUp")]
        async public Task<ServiceResponse<bool>> SignUp(Admins.Admin admin)
        {
           
            var response = new ServiceResponse<bool>();
            

            var data = await service.AddAsync(admin);
            response.Data = data;
            response.IsSuccess = true;
            return response;
        }
    }
}

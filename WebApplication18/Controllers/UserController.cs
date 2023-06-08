using AutoMapper;
using IService;
using Microsoft.AspNetCore.Mvc;
using Model;
using DTO;
using System.Reflection.Metadata.Ecma335;

namespace WebApplication18.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class UserController: ControllerBase
    {
        public readonly IService<User> service;
        private readonly IMapper _mapper;

        public UserController(IService<User> service, IMapper mapper)
        {
            this.service = service;
            this._mapper = mapper;
        }


        [HttpPost("SignUp")]
       async public Task<ServiceResponse<bool>> SignUp(UserDto user)
        {
            var x =  _mapper.Map<User>(user);
            var response = new ServiceResponse<bool>();
             
            var data = await service.AddAsync(x);
            response.Data = data;
            response.IsSuccess = true;
            return response;
        }
        [HttpGet("getAll")]
        async public Task<ServiceResponse<List<User>>> getAll()
        {
                var response = new ServiceResponse<List<User>>();
            try
            {
                var x = await service.getAll();
                response.Data = x;
                response.IsSuccess = true;


                return response;
            }
            catch(Exception ex)
            {
                response.Errors = ex.Message;
                return response;
                 
            }
        }

    }
}

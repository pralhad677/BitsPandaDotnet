using IService;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace WebApplication18.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class UserController: ControllerBase
    {
        public readonly IService<User> service;

        public UserController(IService<User> service)
        {
            this.service = service;
        }


        [HttpPost("SignUp")]
       async public Task<ServiceResponse<bool>> SignUp(User user)
        {
            var response = new ServiceResponse<bool>();
             
            var data = await service.AddAsync(user);
            response.Data = data;
            response.IsSuccess = true;
            return response;
        }

    }
}

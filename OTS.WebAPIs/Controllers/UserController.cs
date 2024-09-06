using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OTS.Common.RequestModels.User;
using OTS.Core.Repositories;

namespace OTS.WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        [Authorize(Roles = "Admin")]
       // using Identity Policy Principle
       //[Authorize(Policy = "UserRolePolicy")] 
        public async Task<IActionResult> GetAllUsers()
        {
            var user = await userRepository.GetAllUsers();
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserDetails(SaveUserRequestModel Request)
        {
            var result = await  userRepository.AddUserDetails(Request);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserDetails( [FromBody] UpdateUserRequestModel Request)
        {
            var result = await userRepository.UpdateUserDetails(Request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserDetailsById(string UserId)
        {
            var result = await userRepository.GetUserDetailsById(UserId);
            return Ok(result);
        }
    }
}
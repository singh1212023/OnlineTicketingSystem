using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OTS.Common.RequestModels.Account;
using OTS.Common.RequestModels.User;
using OTS.Core.Repositories;
using OTS.Repository.Interface.Account;

namespace OTS.WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository accountRepository;

        public AccountController(IAccountRepository _accountRepository)
        {
            accountRepository = _accountRepository;
        }
      

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginRequestModel Request)
        {
            var result = await accountRepository.AuthenticateUser(Request);
            return Ok(result);
        }

    }
}

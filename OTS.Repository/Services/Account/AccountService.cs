using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OTS.Common.Helpers;
using OTS.Common.RequestModels.Account;
using OTS.Common.RequestModels.User;
using OTS.Common.ResponseModel;
using OTS.Core.Entities.Account;
using OTS.Infrastructure.Data;
using OTS.Repository.Interface.Account;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OTS.Repository.Services.Account
{
    public class AccountService : IAccountRepository
    {

        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public IConfiguration configuration;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signingManager;

        public AccountService(ApplicationDbContext _context, IMapper _mapper, IConfiguration _configuration, UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signingManager)
        {
            context = _context;
            mapper = _mapper;
            configuration = _configuration;
            userManager = _userManager;
            signingManager = _signingManager;


        }
        public async Task<GenericBaseResult<UserRequestModel>> AuthenticateUser(LoginRequestModel Request)
        {
            var response = new GenericBaseResult<UserRequestModel>(null);
            try
            {
                if (Request == null)
                    throw new Exception("Email and Password is Required");

                if(string.IsNullOrEmpty(Request.UserEmail))
                    throw new Exception("Email  is Required");

                if (string.IsNullOrEmpty(Request.UserPassword))
                    throw new Exception(" Password is Required");


                var checkUser = await userManager.FindByEmailAsync(Request.UserEmail);
                if (checkUser == null)
                    throw new Exception(" User does not exists");
                
                if (checkUser != null)
                {
                    var checkPassword = await userManager.CheckPasswordAsync(checkUser, Request.UserPassword);
                    if (checkPassword ==true)
                    {
                        response.Message = ResponseMessage.ValidUser;
                        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                        var roles = await userManager.GetRolesAsync(checkUser);

                        var claims = new List<Claim>
                            {
                            new Claim(ClaimTypes.Email,Request.UserEmail),
                            };

                        foreach (var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));
                        }

                        var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                        configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.Now.AddMinutes(120),
                            signingCredentials: credentials);

                        var stringtoken = new JwtSecurityTokenHandler().WriteToken(token);

                        var result = new UserRequestModel
                        {
                            UserId = checkUser.Id.ToString(),
                            AccessToken = stringtoken,
                            ExpiresIn = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                            FirstName = checkUser.FirstName,
                            LastName = checkUser.LastName
                        };

                        response.Result = result;
                        return response;

                    }
                    else
                    {
                        response.Message = ResponseMessage.IncorrectPassowrd;
                        return response;
                    }
                }
                else
                {
                    response.Message = ResponseMessage.NotFound;

                }
                           
                 return response;

            }
            catch (Exception ex)
            {
                var result = new GenericBaseResult<UserRequestModel>(null);
                result.AddExceptionLog(ex);
                return result;

            }


        }
        
    }


}
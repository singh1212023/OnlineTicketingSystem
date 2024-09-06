using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OTS.Common.Helpers;
using OTS.Common.RequestModels.User;
using OTS.Common.ResponseModel;
using OTS.Core.Entities.Account;
using OTS.Core.Repositories;

using OTS.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using AutoMapper;
using System.Runtime.Intrinsics.X86;
using OTS.Common.Contants;

namespace OTS.Repository.Services
{
    public class UserService : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
       
        public UserService(ApplicationDbContext _context, UserManager<ApplicationUser> _userManager, IMapper _mapper )
        {
            context = _context;
            userManager = _userManager;
            mapper=_mapper;
        }

        public async Task<GenericBaseResult<List<GetAllUserRequestModel>>> GetAllUsers()
        {
            var response = new GenericBaseResult<List<GetAllUserRequestModel>>(null);

            try
            {
                var users =  context.AspNetUser
                                 .Where(u => !u.IsDeleted)
                                 .ToList();
                var userData = mapper.Map<List<GetAllUserRequestModel>>(users);

                if (users == null)
                {
                    response.Message = ResponseMessage.NotFound;
                }
                else
                {
                    response.Message = ResponseMessage.Success;
                }
                response.Result = userData;
            }
            catch (Exception ex)
            {
                var result = new GenericBaseResult<List<GetAllUserRequestModel>>(null);
                result.AddExceptionLog(ex);
                return result;
            }
            return response;
            
        }

        public async Task<GenericBaseResult<GetUserByIdRequestModel>> GetUserDetailsById(string Id)
        {
            var response = new GenericBaseResult<GetUserByIdRequestModel>(null);
            try
            {
                var user = context.AspNetUser.FirstOrDefault(u => u.Id == Id);
                var result = mapper.Map<GetUserByIdRequestModel>(user);

                if (user == null)
                {
                    response.Message = ResponseMessage.NotFound;
                }
                else
                {
                    response.Message = ResponseMessage.Success;
                }
                response.Result = result;
                return response;

            }
            catch (Exception ex)
            {
                var result = new GenericBaseResult<GetUserByIdRequestModel>(null);
                result.AddExceptionLog(ex);
                return result;
            }

        }

        public async Task<GenericBaseResult<SaveUserRequestModel>> AddUserDetails(SaveUserRequestModel Request)
        {
            var response = new GenericBaseResult<SaveUserRequestModel>(null);
            try
            {
                var checkUserEmail = await context.AspNetUser.FirstOrDefaultAsync(x => x.Email.ToLower() == Request.Email.ToLower() && x.IsDeleted == false);
                if (checkUserEmail != null)
                {
                    throw new Exception(ResponseMessage.UserEmailExist);

                }
                var newApplicationUser = mapper.Map<ApplicationUser>(Request);
                var result =  await userManager.CreateAsync(newApplicationUser, Request.Passsword);
                
                if (!result.Succeeded)
                {
                    throw new Exception("User creation failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
                //var userRoleMap = await userManager.AddToRoleAsync(newApplicationUser,Request.RoleId);

                var userRoleMapping = new ApplicationUserRole()
                {
                    RoleId = Request.RoleId,
                    UserId = Request.Id,

                };
                context.AspNetUserRole.Add(userRoleMapping);
                context.SaveChanges();
                response.Message = ResponseMessage.RecordSaved;
                return response;
            }
            catch (Exception ex)
            {
                var result = new GenericBaseResult<SaveUserRequestModel>(null);
                result.AddExceptionLog(ex);
                return result;
                
            }
        }
   
        public async Task<GenericBaseResult<UpdateUserRequestModel>> UpdateUserDetails(UpdateUserRequestModel Request)
        {
            var response = new GenericBaseResult<UpdateUserRequestModel>(null);

            try
            {
                if (!string.IsNullOrEmpty(Request.Id))
                {
                    var existingUser = await context.AspNetUser.FirstOrDefaultAsync(u => u.Id == Request.Id);
                    if (existingUser != null)
                    {
                        mapper.Map(Request, existingUser);
                        context.AspNetUser.Update(existingUser);
                        await context.SaveChangesAsync();
                        response.Message = ResponseMessage.RecordUpdated;

                    }
                    else
                    {
                        response.Message = ResponseMessage.NotFound;
                    }

                }

            }
            catch (Exception ex)
            {
                var result = new GenericBaseResult<UpdateUserRequestModel>(null);
                result.AddExceptionLog(ex);
                return result;

            }
            return response; 

        }

      

    }
}













//var checkUsers = await context.AspNetUser.FirstOrDefaultAsync(x => x.UserName.ToLower() == Data.UserName.ToLower() && x.IsDeleted == false);
//if (checkUsers != null)
//{
//    respose.Status = 0;
//    respose.StatusCode = ResponseCode.Ok;
//    respose.Message = ResponseMessage.UserExist;

//}
//var checkUserEmail = await  context.AspNetUser.FirstOrDefaultAsync(x => x.Email.ToLower() == Data.Email.ToLower() && x.IsDeleted == false);
//if (checkUserEmail != null)
//{
//    respose.Status = 0;
//    respose.StatusCode = ResponseCode.Ok;
//    respose.Message = ResponseMessage.UserEmailExist;

//}
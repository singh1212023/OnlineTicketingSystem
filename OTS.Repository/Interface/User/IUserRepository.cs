using OTS.Common.Helpers;
using OTS.Common.RequestModels.User;
using OTS.Core.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS.Core.Repositories
{
    public interface IUserRepository
    {
        Task<GenericBaseResult<List<GetAllUserRequestModel>>> GetAllUsers();
        Task<GenericBaseResult<SaveUserRequestModel>> AddUserDetails(SaveUserRequestModel Request);
        Task<GenericBaseResult<UpdateUserRequestModel>> UpdateUserDetails(UpdateUserRequestModel Request);
        Task<GenericBaseResult<GetUserByIdRequestModel>> GetUserDetailsById(string Id);
    }
}

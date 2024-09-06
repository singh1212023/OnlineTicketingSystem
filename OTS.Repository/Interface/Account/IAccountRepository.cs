using AutoMapper;
using OTS.Common.Helpers;
using OTS.Common.RequestModels.Account;
using OTS.Common.RequestModels.User;
using OTS.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS.Repository.Interface.Account
{
    public interface IAccountRepository
    {
      
     
        Task<GenericBaseResult<UserRequestModel>> AuthenticateUser(LoginRequestModel Data);
    }
}

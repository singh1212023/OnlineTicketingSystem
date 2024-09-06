using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS.Common.RequestModels.Account
{
    public  class LoginRequestModel
    {
        public string UserEmail { get; set; } = null!;

        public string UserPassword { get; set; } = null!;
    }
}

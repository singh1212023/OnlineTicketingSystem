using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS.Common.RequestModels.Account
{
    public class UserRequestModel
    {
        public string UserId { get; set; }
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;
    }
}

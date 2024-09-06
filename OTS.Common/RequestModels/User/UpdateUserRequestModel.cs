using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS.Common.RequestModels.User
{
    public class UpdateUserRequestModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ConfirmEmail { get; set; }
        public bool EmailConfirmed { get; set; } = false;
        public string PhoneNumber { get; set; }
        public string ConfirmPhone { get; set; }
        public bool PhoneNumberConfirmed { get; set; } = false;
        public bool LockoutEnabled { get; set; } = false;
        public int AccessFailedCount { get; set; } = 0;
       

    }
}

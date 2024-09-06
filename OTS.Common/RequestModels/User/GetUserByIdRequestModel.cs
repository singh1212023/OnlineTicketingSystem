using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS.Common.RequestModels.User
{
    public class GetUserByIdRequestModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ConfirmEmail { get; set; }

        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public string ConfirmPhone { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; } 
        public bool IsDeleted { get; set; } 
    }
}

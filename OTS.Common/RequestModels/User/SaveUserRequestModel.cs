namespace OTS.Common.RequestModels.User
{
    public class SaveUserRequestModel : GuidModelBase
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; } = false;
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; } = true;
        public bool LockoutEnabled { get; set; } = true;
        public int AccessFailedCount { get; set; } = 0;
        public string Passsword { get; set; }
        public string RoleId { get; set; }


    }


}

using OTS.Common.Contants;
using OTS.Core.Entities.Account;
using OTS.Infrastructure.Data;

namespace OTS.WebAPIs.Helper
{
    public static class DbInitilizer
    {

        public static void Initialize(ApplicationDbContext _context)
        {
            _context.Database.EnsureCreated();
            SeedRoles(_context);
        }
        private static void SeedRoles(ApplicationDbContext _context)
        {

            const string AdminRoleGuid = "363ca96c-d99c-4273-98bc-45dcf393205f";
            const string UserRoleGuid = "7a597e4e-feca-43f3-a144-95de6b7af934";

            if (!_context.AspNetRole.Any(r => r.Name == Constant.Admin))
            {
                var adminRoleEntry = new ApplicationRole()
                {
                    Id = AdminRoleGuid,
                    Name = Constant.Admin,
                    NormalizedName = Constant.Admin.ToUpper(),
                };
                _context.AspNetRole.Add(adminRoleEntry);
                _context.SaveChanges();

            }
            if (!_context.AspNetRole.Any(r => r.Name == Constant.User))
            {
                var userRoleEntry = new ApplicationRole()
                {
                    Id = UserRoleGuid,
                    Name = Constant.User,
                    NormalizedName = Constant.User.ToUpper(),
                };

                _context.AspNetRole.Add(userRoleEntry);
                _context.SaveChanges();
            }





        }
    }
}

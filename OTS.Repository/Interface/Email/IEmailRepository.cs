using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS.Repository.Interface.Email
{
    public interface IEmailRepository
    {
        //email TASK For Single User Working
        public string SendEmailToAdmin(string Email, string Subject, string emailhtml);

        //email TASK For Multiple UsersWorking below
        //Task<string> SendEmailToAdminAsync(string email, string subject, string emailHtml);
    }
}

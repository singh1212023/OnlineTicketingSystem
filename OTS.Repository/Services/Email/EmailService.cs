using Microsoft.Extensions.Configuration;
using OTS.Common.ResponseModel;
using OTS.Repository.Interface.Email;
using System.Net;
using System.Net.Mail;

namespace OTS.Repository.Services.Email
{
    public class EmailService : IEmailRepository
    {
        private readonly IConfiguration config;
        public EmailService(IConfiguration _config)
        {
            config = _config;
        }

        //email TASK For Single User Working
        public string SendEmailToAdmin(string Email, string Subject, string emailhtml)
        { 
            EmailResponseModel response=new EmailResponseModel();
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                var smtpServer = config["EmailConfiguration:SmtpServer"];
                var smtpPort = int.Parse(config["EmailConfiguration:Port"]);
                var smtpUsername = config["EmailConfiguration:Username"];
                var smtpPassword = config["EmailConfiguration:Password"];

                using (var client = new SmtpClient(smtpServer, smtpPort))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    client.EnableSsl = true;

                    var mail = new MailMessage
                    {
                        From = new MailAddress(smtpUsername),
                        To = { new MailAddress(Email) },
                        Subject = Subject,
                        Body = $"{emailhtml}",
                        IsBodyHtml = true
                    };

                    client.Send(mail);
                    return response.Message = ResponseMessage.EmailSentSucces;
                   
                    //return "Successful";
                }
            }
            catch (Exception ex)
            {
                return response.Message= ex.Message;
               // return $"Failed to send Template to email. Error: {ex.Message ?? "Unknown error"}";
            }

        }



        //email TASK For Multiple UsersWorking


        //public async Task<string> SendEmailToAdminAsync(string email, string subject, string emailHtml)
        //{
        //    try
        //    {
        //        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

        //        var smtpServer = config["EmailConfiguration:SmtpServer"];
        //        var smtpPort = int.Parse(config["EmailConfiguration:Port"]);
        //        var smtpUsername = config["EmailConfiguration:Username"];
        //        var smtpPassword = config["EmailConfiguration:Password"];

        //        using (var client = new SmtpClient(smtpServer, smtpPort))
        //        {
        //            client.UseDefaultCredentials = false;
        //            client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
        //            client.EnableSsl = true;

        //            var mail = new MailMessage
        //            {
        //                From = new MailAddress(smtpUsername),
        //                To = { new MailAddress(email) },
        //                Subject = subject,
        //                Body = emailHtml,
        //                IsBodyHtml = true
        //            };

        //            await client.SendMailAsync(mail);

        //            return "Successful";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return $"Failed to send email. Error: {ex.Message ?? "Unknown error"}";
        //    }
        //}
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS.Common.ResponseModel
{
    public class ResponseMessage
    {

        public const string Error = "Some internal error occured";
        public const string UserExist = "UserName Already Exists";
        public const string UserEmailExist = "Email Is Already In Use";
        public const string RecordUpdated = "Record Updated Successfully";
        public const string RecordSaved = "Record Saved Successfully";
        public const string NotFound = "No Record Found";
        public const string Success = " Data Fetched Successfully";
        public const string ValidUser = "Valid User";
        public const string IncorrectPassowrd = "Incorrect Passowrd";
        public const string EmailSentSucces = "Email Sent Successfully";
        public const string EmailSentFailure = "Unable to send email";
        public const string TicketRaised = "New Ticket Raised";

    }
}

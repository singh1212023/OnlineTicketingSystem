using OTS.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS.Common.RequestModels.Tickets
{
    public  class GetAllTicketByIdRequestModel
    {
        public string TicketNo { get; set; }
        public string TicketName { get; set; }
        public string Description { get; set; }
        public string RequestorId { get; set; }
        public string Priority { get; set; }
        public string Department { get; set; }
        public string Status { get; set; }
        public DateTime SubmittedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }

    }
}

using OTS.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS.Common.RequestModels.Tickets
{
    public class GetAllTicketsRequestModel
    {
        public string Department { get; set; }
        public string Status { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string TicketName { get; set; }
    }
}

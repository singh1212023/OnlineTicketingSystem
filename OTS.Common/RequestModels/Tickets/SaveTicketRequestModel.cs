using OTS.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using OTS.Core.Entities.Enums;

namespace OTS.Common.RequestModels.Ticket
{
    public class SaveTicketRequestModel
    {
        //[StringLength(50)]
        //public string TicketNo { get; set; }
        [StringLength(50)]
        public string TicketName { get; set; }
        [StringLength(200)]
        public string Description { get; set; }

        public string RequestorId { get; set; }
        public Priority Priority { get; set; } 
        public Department Department { get; set; }
        //public Status Status { get; set; } = Status.Answered;


    }
}

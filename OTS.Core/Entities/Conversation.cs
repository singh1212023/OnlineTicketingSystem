using OTS.Common;
using OTS.Core.Entities.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS.Core.Entities
{
    public class Conversation:GuidModelBase
    {
       
        public string TicketId { get; set; }
       
        public string RequestorId { get; set; }
        [StringLength(200)]
        public string Message { get; set; }
        public string SubmittedDate { get; set; }
        public string LastUpdatedDate { get; set; }

        [ForeignKey(nameof(TicketId))]
        public virtual Ticket Ticket { get; set; }
        [ForeignKey(nameof(RequestorId))]
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Attachment> Attchments{ get; set; }


    }
}

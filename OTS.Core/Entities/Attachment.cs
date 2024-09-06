using OTS.Common;
using OTS.Core.Entities.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS.Core.Entities
{
    public  class Attachment:GuidModelBase
    {

        public string TicketId { get; set; }

        public string ConversationId { get; set; }
        public string  FilePath { get; set; }
        public DateTime UploadedDate { get; set; }

        [ForeignKey(nameof(TicketId))]
        public virtual Ticket Ticket { get; set; }
        [ForeignKey(nameof(ConversationId))]
        public virtual Conversation Conversation { get; set; }

    }
}

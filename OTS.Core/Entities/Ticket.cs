using OTS.Common;
using OTS.Common.Enums;
using OTS.Core.Entities.Account;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OTS.Core.Entities
{
    public class Ticket : GuidModelBase
    {
        [StringLength(50)]
        public string TicketNo { get; set; }
        [StringLength(50)]
        public string TicketName { get; set; }
        [StringLength(200)]
        public string Description { get; set; }

        public string RequestorId { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; } = Status.Answered;
        public Department Department { get; set; }
        public DateTime SubmittedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }   
        [ForeignKey(nameof(RequestorId))]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Conversation> Conversations { get; set; }


    }
}

using OTS.Common.Helpers;
using OTS.Common.RequestModels.Ticket;
using OTS.Common.RequestModels.Tickets;
using OTS.Common.RequestModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS.Repository.Interface.Ticket
{
    public interface ITicketRepository
    {
        Task<GenericBaseResult<List<GetAllTicketByIdRequestModel>>> GetAllTicketsByUserId(string Id);
        Task<GenericBaseResult<List<GetAllTicketsRequestModel>>> GetAllTickets(SortingModel Request);
        Task<GenericBaseResult<GetTicketByIdRequestModel>> GetTicketById(string Id);
        Task<GenericBaseResult<SaveTicketRequestModel>> AddTicketDetails(SaveTicketRequestModel Request);
     
        
    }
}


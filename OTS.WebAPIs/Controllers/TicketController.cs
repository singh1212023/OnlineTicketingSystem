using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OTS.Common.Helpers;
using OTS.Common.RequestModels.Ticket;
using OTS.Common.RequestModels.Tickets;
using OTS.Common.RequestModels.User;
using OTS.Core.Repositories;
using OTS.Repository.Interface.Ticket;

namespace OTS.WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository ticketRepository;
        public TicketController(ITicketRepository _ticketRepository)
        {
            ticketRepository = _ticketRepository;
            
        }


        [HttpGet]
        [Route("GetAllTicketsByUserId")]
        public async Task<IActionResult> GetAllTicketsByUserId(string UserId)
        {
            var result = await ticketRepository.GetAllTicketsByUserId(UserId);
            return Ok(result);
        }



        [HttpPost]
        [Route("GetAllTickets")]
        public async Task<IActionResult> GetAllTickets(SortingModel Request)
        {
            var result = await ticketRepository.GetAllTickets(Request);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetTicketById")]
        public async Task<IActionResult> GetTicketById(string TicketId)
        {
            var result = await ticketRepository.GetTicketById(TicketId);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> AddTicketDetails(SaveTicketRequestModel Request)
        {
            var result = await ticketRepository.AddTicketDetails(Request);
            return Ok(result);
        }



    }
}
